using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Application.Features.Documents.Commands;
using ShatteredRealms.Application.Features.Documents.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class DocumentsController : ControllerBase
{
    private static readonly HashSet<string> AllowedExtensions = [".pdf", ".docx"];
    private static readonly HashSet<string> AllowedContentTypes =
    [
        "application/pdf",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    ];
    private const long MaxFileBytes = 20 * 1024 * 1024; // 20 MB

    private readonly IMediator _mediator;
    private readonly IFileStorageService _fileStorage;

    public DocumentsController(IMediator mediator, IFileStorageService fileStorage)
    {
        _mediator    = mediator;
        _fileStorage = fileStorage;
    }

    [RequirePermission(Claims.Permissions.Documents.View)]
    [HttpGet]
    public async Task<ActionResult<List<DocumentDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllDocumentsQuery(), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Documents.View)]
    [HttpGet("{id:int}/download")]
    public async Task<IActionResult> Download(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DownloadDocumentQuery(id), cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);

        var dto = result.Value;
        var relativePath = Path.Combine("documents", dto.StoredFileName);

        try
        {
            var stream = _fileStorage.Open(relativePath);
            return File(stream, dto.ContentType, dto.OriginalFileName);
        }
        catch (FileNotFoundException)
        {
            return Problem(detail: "File not found on disk", statusCode: 404, title: "Document.NotFound");
        }
    }

    [RequirePermission(Claims.Permissions.Documents.Upload)]
    [HttpPost]
    [RequestSizeLimit(MaxFileBytes + 1024)]
    public async Task<ActionResult<DocumentDto>> Upload(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return Problem(detail: "No file provided", statusCode: 400, title: "Document.NoFile");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
            return Problem(detail: DomainErrors.Document.InvalidFileType.Message, statusCode: 400, title: DomainErrors.Document.InvalidFileType.Title);

        if (file.Length > MaxFileBytes)
            return Problem(detail: DomainErrors.Document.FileTooLarge.Message, statusCode: 400, title: DomainErrors.Document.FileTooLarge.Title);

        var uploaderId = User.GetUserId();
        if (string.IsNullOrEmpty(uploaderId))
            return Problem(detail: "User ID cannot be resolved", statusCode: 400, title: "Invalid User");

        var storedName = $"{Guid.NewGuid()}{ext}";
        var relativePath = Path.Combine("documents", storedName);

        await using (var stream = file.OpenReadStream())
        {
            await _fileStorage.SaveAsync(stream, relativePath, cancellationToken);
        }

        var result = await _mediator.Send(
            new SaveDocumentCommand(uploaderId, file.FileName, storedName, file.ContentType, file.Length),
            cancellationToken);

        if (result.IsFailure)
        {
            await _fileStorage.DeleteAsync(relativePath, cancellationToken);
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return CreatedAtAction(nameof(Download), new { id = result.Value.Id }, result.Value);
    }

    [RequirePermission(Claims.Permissions.Documents.Delete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteDocumentCommand(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }
}
