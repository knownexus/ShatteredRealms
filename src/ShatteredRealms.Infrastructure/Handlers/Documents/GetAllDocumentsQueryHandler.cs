using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Application.Features.Documents.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Documents;

public sealed class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, Result<List<DocumentDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllDocumentsQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<DocumentDto>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Document
            .Include(d => d.UploadedBy)
            .OrderByDescending(d => d.UploadedAt)
            .Select(d => new DocumentDto
            {
                Id               = d.Id,
                OriginalFileName = d.OriginalFileName,
                ContentType      = d.ContentType,
                FileSizeBytes    = d.FileSizeBytes,
                UploadedById     = d.UploadedById,
                UploadedByName   = d.UploadedBy.UserName ?? string.Empty,
                UploadedAt       = d.UploadedAt,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(list);
    }
}
