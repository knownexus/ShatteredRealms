using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class CreateWikiPageCommandHandler
    : IRequestHandler<CreateWikiPageCommand, Result<WikiPageDto>>
{
    private readonly IWikiService _wikiService;
    private readonly IAnalyticsService _analytics;

    public CreateWikiPageCommandHandler(IWikiService wikiService, IAnalyticsService analytics)
    {
        _wikiService = wikiService;
        _analytics = analytics;
    }

    public async Task<Result<WikiPageDto>> Handle(
        CreateWikiPageCommand request, CancellationToken cancellationToken)
    {
        var result = await _wikiService.CreatePageAsync(
            request.RequestingUserId,
            new CreateWikiPageRequest(request.Title, request.Content, request.RevisionNote, request.CategoryIds),
            cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryEventType.WikiPageCreated, request.RequestingUserId, string.Empty,
                targetId: result.Value.Id.ToString(), targetName: result.Value.Title, cancellationToken: cancellationToken);
        }

        return result;
    }
}
