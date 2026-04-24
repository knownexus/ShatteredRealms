using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Application.Features.Documents.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Documents;

public sealed class DownloadDocumentQueryHandler : IRequestHandler<DownloadDocumentQuery, Result<DocumentDownloadDto>>
{
    private readonly ApplicationDbContext _context;

    public DownloadDocumentQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<DocumentDownloadDto>> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        var doc = await _context.Document
            .Where(d => d.Id == request.Id)
            .Select(d => new DocumentDownloadDto
            {
                StoredFileName   = d.StoredFileName,
                OriginalFileName = d.OriginalFileName,
                ContentType      = d.ContentType,
            })
            .FirstOrDefaultAsync(cancellationToken);

        return doc is not null
            ? Result.Success(doc)
            : Result.Failure<DocumentDownloadDto>(DomainErrors.Document.NotFound);
    }
}
