using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class UpdateForumCategoryCommandHandler
    : IRequestHandler<UpdateForumCategoryCommand, Result<ForumCategoryDto>>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public UpdateForumCategoryCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result<ForumCategoryDto>> Handle(
        UpdateForumCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdateCategoryAsync(
            request.CategoryId,
            new UpdateForumCategoryRequest(request.Name, request.Description, request.SortOrder),
            cancellationToken);

        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryActionType.ForumCategoryUpdated, request.ActorId, string.Empty,
                targetId: request.CategoryId.ToString(), targetName: request.Name, cancellationToken: cancellationToken);
        }

        return result;
    }
}
