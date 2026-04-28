using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class UpdateForumThreadCommandHandler
    : IRequestHandler<UpdateForumThreadCommand, Result<ForumThreadDto>>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public UpdateForumThreadCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result<ForumThreadDto>> Handle(
        UpdateForumThreadCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdateThreadAsync(
            request.ThreadId,
            request.RequestingUserId,
            new UpdateForumThreadRequest(request.Title, request.IsPinned, request.IsLocked),
            cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryActionType.ForumThreadUpdated, request.RequestingUserId, string.Empty,
                targetId: request.ThreadId.ToString(), targetName: request.Title, cancellationToken: cancellationToken);
        }

        return result;
    }
}
