using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumPostCommandHandler : IRequestHandler<DeleteForumPostCommand, Result>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public DeleteForumPostCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteForumPostCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.DeletePostAsync(request.PostId, request.RequestingUserId, cancellationToken);
        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryEventType.ForumPostDeleted, request.RequestingUserId, string.Empty,
                targetId: request.PostId.ToString(), cancellationToken: cancellationToken);
        }
        return result;
    }
}
