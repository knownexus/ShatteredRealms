using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumPostAsAdminCommandHandler : IRequestHandler<DeleteForumPostAsAdminCommand, Result>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public DeleteForumPostAsAdminCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteForumPostAsAdminCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.DeletePostAsAdminAsync(request.PostId, cancellationToken);
        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryActionType.ForumPostDeleted, request.ActorId, string.Empty,
                targetId: request.PostId.ToString(), cancellationToken: cancellationToken);
        }
        return result;
    }
}
