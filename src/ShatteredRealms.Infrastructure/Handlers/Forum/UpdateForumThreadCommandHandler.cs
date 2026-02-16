using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class UpdateForumThreadCommandHandler
    : IRequestHandler<UpdateForumThreadCommand, Result<ForumThreadDto>>
{
    private readonly IForumService _forumService;
    public UpdateForumThreadCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumThreadDto>> Handle(
        UpdateForumThreadCommand request, CancellationToken cancellationToken)
        => _forumService.UpdateThreadAsync(
            request.ThreadId,
            request.RequestingUserId,
            new UpdateForumThreadRequest(request.Title, request.IsPinned, request.IsLocked),
            cancellationToken);
}
