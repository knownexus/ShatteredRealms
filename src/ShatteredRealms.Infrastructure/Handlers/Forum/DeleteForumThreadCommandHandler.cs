using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumThreadCommandHandler : IRequestHandler<DeleteForumThreadCommand, Result>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public DeleteForumThreadCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteForumThreadCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.DeleteThreadAsync(request.ThreadId, cancellationToken);
        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.ForumThreadDeleted, request.ActorId, string.Empty,
                targetId: request.ThreadId.ToString(), cancellationToken: cancellationToken);
        }
        return result;
    }
}
