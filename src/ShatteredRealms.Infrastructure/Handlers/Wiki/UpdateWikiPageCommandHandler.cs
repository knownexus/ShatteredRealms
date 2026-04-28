using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class UpdateWikiPageCommandHandler
    : IRequestHandler<UpdateWikiPageCommand, Result<WikiPageDto>>
{
    private readonly IWikiService _wikiService;
    private readonly IAnalyticsService _analytics;

    public UpdateWikiPageCommandHandler(IWikiService wikiService, IAnalyticsService analytics)
    {
        _wikiService = wikiService;
        _analytics = analytics;
    }

    public async Task<Result<WikiPageDto>> Handle(UpdateWikiPageCommand request, CancellationToken cancellationToken)
    {
        var result = await _wikiService.UpdatePageAsync(request.PageId,
                                            request.RequestingUserId,
                                            new UpdateWikiPageRequest(request.Title, request.Content
                                                                    , request.RevisionNote, request.CategoryIds),
                                            cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryActionType.WikiPageUpdated, request.RequestingUserId, string.Empty,
                targetId: request.PageId.ToString(), targetName: result.Value.Title, cancellationToken: cancellationToken);
        }

        return result;
    }
}
