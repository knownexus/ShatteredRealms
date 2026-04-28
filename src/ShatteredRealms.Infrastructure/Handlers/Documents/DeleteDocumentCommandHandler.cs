using MediatR;
using ShatteredRealms.Application.Features.Documents.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Documents;

public sealed class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public DeleteDocumentCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context   = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Document.FindAsync([request.Id], cancellationToken);
        if (doc is null)
        {
            return Result.Failure(DomainErrors.Document.NotFound);
        }

        doc.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.DocumentDeleted, request.ActorId, string.Empty,
                targetId: doc.Id.ToString(), targetName: doc.OriginalFileName,
                cancellationToken: cancellationToken);
        }

        return Result.Success();
    }
}
