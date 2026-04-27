using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class CreateForumThreadCommandHandler
    : IRequestHandler<CreateForumThreadCommand, Result<ForumThreadDto>>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public CreateForumThreadCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result<ForumThreadDto>> Handle(
        CreateForumThreadCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.CreateThreadAsync(
            request.RequestingUserId,
            new CreateForumThreadRequest(request.CategoryId, request.Title, request.InitialPostContent),
            cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryEventType.ForumThreadCreated, request.RequestingUserId, string.Empty,
                targetId: result.Value.Id.ToString(), targetName: result.Value.Title, cancellationToken: cancellationToken);
        }

        return result;
    }
}
