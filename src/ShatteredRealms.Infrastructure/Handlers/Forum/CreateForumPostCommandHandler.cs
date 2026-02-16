using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class CreateForumPostCommandHandler
    : IRequestHandler<CreateForumPostCommand, Result<ForumPostDto>>
{
    private readonly IForumService _forumService;
    public CreateForumPostCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumPostDto>> Handle(
        CreateForumPostCommand request, CancellationToken cancellationToken)
        => _forumService.CreatePostAsync(
            request.RequestingUserId,
            new CreateForumPostRequest(request.ThreadId, request.Content),
            cancellationToken);
}
