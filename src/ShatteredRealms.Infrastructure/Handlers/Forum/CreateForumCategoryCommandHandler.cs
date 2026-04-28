using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class CreateForumCategoryCommandHandler
    : IRequestHandler<CreateForumCategoryCommand, Result<ForumCategoryDto>>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public CreateForumCategoryCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result<ForumCategoryDto>> Handle(
        CreateForumCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.CreateCategoryAsync(
            request.RequestingUserId,
            new CreateForumCategoryRequest(request.Name, request.Description, request.SortOrder),
            cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryActionType.ForumCategoryCreated, request.RequestingUserId, string.Empty,
                targetId: result.Value.Id.ToString(), targetName: result.Value.Name, cancellationToken: cancellationToken);
        }

        return result;
    }
}
