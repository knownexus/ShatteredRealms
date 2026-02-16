using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class CreateForumThreadCommandHandler
    : IRequestHandler<CreateForumThreadCommand, Result<ForumThreadDto>>
{
    private readonly IForumService _forumService;
    public CreateForumThreadCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumThreadDto>> Handle(
        CreateForumThreadCommand request, CancellationToken cancellationToken)
        => _forumService.CreateThreadAsync(
            request.RequestingUserId,
            new CreateForumThreadRequest(request.CategoryId, request.Title, request.InitialPostContent),
            cancellationToken);
}
