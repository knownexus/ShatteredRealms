using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class UpdateForumPostCommandHandler
    : IRequestHandler<UpdateForumPostCommand, Result<ForumPostDto>>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public UpdateForumPostCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result<ForumPostDto>> Handle(
        UpdateForumPostCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdatePostAsync(request.PostId, request.RequestingUserId, request.Content, cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryEventType.ForumPostUpdated, request.RequestingUserId, string.Empty,
                targetId: request.PostId.ToString(), cancellationToken: cancellationToken);
        }

        return result;
    }
}
