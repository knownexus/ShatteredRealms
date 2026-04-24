using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Application.Features.Events.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class EventsController : ControllerBase
{
    private static readonly string[] AllowedBannerExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxBannerBytes = 5 * 1024 * 1024; // 5 MB

    private readonly IMediator _mediator;
    private readonly IFileStorageService _fileStorage;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IMediator mediator, IFileStorageService fileStorage, ILogger<EventsController> logger)
    {
        _mediator    = mediator;
        _fileStorage = fileStorage;
        _logger      = logger;
    }

    [RequirePermission(Claims.Permissions.Events.View)]
    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> GetAll(
        [FromQuery] bool upcomingOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllEventsQuery(User.GetUserId(), upcomingOnly), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Events.Register)]
    [HttpGet("mine")]
    public async Task<ActionResult<List<EventDto>>> GetMine(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new GetMyEventsQuery(userId), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Events.View)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetEventByIdQuery(id, User.GetUserId()), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Events.Create)]
    [HttpPost]
    public async Task<ActionResult<EventDto>> Create([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(
            new CreateEventCommand(userId, request.Title, request.Description, request.StartsAt, request.EndsAt, request.Location, request.MemberCap),
            cancellationToken);

        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [RequirePermission(Claims.Permissions.Events.Update)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EventDto>> Update(int id, [FromBody] UpdateEventRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UpdateEventCommand(id, request.Title, request.Description, request.StartsAt, request.EndsAt, request.Location, request.MemberCap),
            cancellationToken);

        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Events.Delete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteEventCommand(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Events.Register)]
    [HttpPost("{id:int}/register")]
    public async Task<IActionResult> Register(int id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new RegisterForEventCommand(id, userId), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok();
    }

    [RequirePermission(Claims.Permissions.Events.Register)]
    [HttpDelete("{id:int}/register")]
    public async Task<IActionResult> CancelRegistration(int id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new CancelRegistrationCommand(id, userId), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Events.Update)]
    [HttpPost("{id:int}/banner")]
    public async Task<IActionResult> UploadBanner(int id, IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length > MaxBannerBytes)
            return Problem(detail: "Banner image must be 5 MB or smaller", statusCode: 400, title: "File Too Large");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedBannerExtensions.Contains(ext))
            return Problem(detail: "Only jpg, png, and webp images are allowed", statusCode: 400, title: "Invalid File Type");

        var relativePath = $"events/{id}/banner{ext}";

        await using var stream = file.OpenReadStream();
        await _fileStorage.SaveAsync(stream, relativePath, cancellationToken);

        var result = await _mediator.Send(new SetEventBannerCommand(id, relativePath), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(new { path = relativePath });
    }
}
