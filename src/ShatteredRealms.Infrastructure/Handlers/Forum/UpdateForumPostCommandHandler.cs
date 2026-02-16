using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class UpdateForumPostCommandHandler
    : IRequestHandler<UpdateForumPostCommand, Result<ForumPostDto>>
{
    private readonly IForumService _forumService;
    public UpdateForumPostCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumPostDto>> Handle(
        UpdateForumPostCommand request, CancellationToken cancellationToken)
        => _forumService.UpdatePostAsync(request.PostId, request.RequestingUserId, request.Content, cancellationToken);
}
