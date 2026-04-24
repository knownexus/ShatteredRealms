using MediatR;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Application.Features.Documents.Commands;
using ShatteredRealms.Domain.Entities.Document;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Documents;

public sealed class SaveDocumentCommandHandler : IRequestHandler<SaveDocumentCommand, Result<DocumentDto>>
{
    private readonly ApplicationDbContext _context;

    public SaveDocumentCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<DocumentDto>> Handle(SaveDocumentCommand request, CancellationToken cancellationToken)
    {
        var doc = new Document
        {
            OriginalFileName = request.OriginalFileName,
            StoredFileName   = request.StoredFileName,
            ContentType      = request.ContentType,
            FileSizeBytes    = request.FileSizeBytes,
            UploadedById     = request.UploadedById,
            UploadedAt       = DateTime.UtcNow,
        };

        _context.Document.Add(doc);
        await _context.SaveChangesAsync(cancellationToken);

        var uploader = await _context.Users.FindAsync([request.UploadedById], cancellationToken);

        return Result.Success(new DocumentDto
        {
            Id               = doc.Id,
            OriginalFileName = doc.OriginalFileName,
            ContentType      = doc.ContentType,
            FileSizeBytes    = doc.FileSizeBytes,
            UploadedById     = doc.UploadedById,
            UploadedByName   = uploader?.UserName ?? string.Empty,
            UploadedAt       = doc.UploadedAt,
        });
    }
}
