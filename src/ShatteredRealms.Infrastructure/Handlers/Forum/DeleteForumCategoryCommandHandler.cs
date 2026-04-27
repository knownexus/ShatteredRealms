using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumCategoryCommandHandler : IRequestHandler<DeleteForumCategoryCommand, Result>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public DeleteForumCategoryCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteForumCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.DeleteCategoryAsync(request.CategoryId, cancellationToken);
        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.ForumCategoryDeleted, request.ActorId, string.Empty,
                targetId: request.CategoryId.ToString(), cancellationToken: cancellationToken);
        }
        return result;
    }
}
