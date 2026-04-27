using MediatR;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class DeleteWikiPageCommandHandler : IRequestHandler<DeleteWikiPageCommand, Result>
{
    private readonly IWikiService _wikiService;
    private readonly IAnalyticsService _analytics;

    public DeleteWikiPageCommandHandler(IWikiService wikiService, IAnalyticsService analytics)
    {
        _wikiService = wikiService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteWikiPageCommand request, CancellationToken cancellationToken)
    {
        var result = await _wikiService.DeletePageAsync(request.PageId, cancellationToken);
        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.WikiPageDeleted, request.ActorId, string.Empty,
                targetId: request.PageId.ToString(), cancellationToken: cancellationToken);
        }
        return result;
    }
}
