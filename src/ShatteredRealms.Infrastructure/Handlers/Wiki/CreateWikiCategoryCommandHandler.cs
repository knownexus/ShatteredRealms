using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class CreateWikiCategoryCommandHandler
    : IRequestHandler<CreateWikiCategoryCommand, Result<WikiCategoryDto>>
{
    private readonly IWikiService _wikiService;
    private readonly IAnalyticsService _analytics;

    public CreateWikiCategoryCommandHandler(IWikiService wikiService, IAnalyticsService analytics)
    {
        _wikiService = wikiService;
        _analytics = analytics;
    }

    public async Task<Result<WikiCategoryDto>> Handle(
        CreateWikiCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _wikiService.CreateCategoryAsync(
            new CreateWikiCategoryRequest(request.Name, request.Description),
            cancellationToken);

        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.WikiCategoryCreated, request.ActorId, string.Empty,
                targetId: result.Value.Id.ToString(), targetName: result.Value.Name, cancellationToken: cancellationToken);
        }

        return result;
    }
}
