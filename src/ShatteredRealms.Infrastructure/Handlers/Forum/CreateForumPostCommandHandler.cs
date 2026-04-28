using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class CreateForumPostCommandHandler
    : IRequestHandler<CreateForumPostCommand, Result<ForumPostDto>>
{
    private readonly IForumService _forumService;
    private readonly IAnalyticsService _analytics;

    public CreateForumPostCommandHandler(IForumService forumService, IAnalyticsService analytics)
    {
        _forumService = forumService;
        _analytics = analytics;
    }

    public async Task<Result<ForumPostDto>> Handle(
        CreateForumPostCommand request, CancellationToken cancellationToken)
    {
        var result = await _forumService.CreatePostAsync(
            request.RequestingUserId,
            new CreateForumPostRequest(request.ThreadId, request.Content),
            cancellationToken);

        if (result.IsSuccess)
        {
            await _analytics.TrackAsync(TelemetryActionType.ForumPostCreated, request.RequestingUserId, string.Empty,
                targetId: result.Value.Id.ToString(),
                details: $"In thread {request.ThreadId}", cancellationToken: cancellationToken);
        }

        return result;
    }
}
