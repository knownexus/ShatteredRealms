using MediatR;
using ShatteredRealms.Application.Features.Documents.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Documents;

public sealed class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteDocumentCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Document.FindAsync([request.Id], cancellationToken);
        if (doc is null)
            return Result.Failure(DomainErrors.Document.NotFound);

        doc.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
