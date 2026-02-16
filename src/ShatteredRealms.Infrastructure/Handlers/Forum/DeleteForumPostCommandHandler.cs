using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumPostCommandHandler : IRequestHandler<DeleteForumPostCommand, Result>
{
    private readonly IForumService _forumService;
    public DeleteForumPostCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result> Handle(DeleteForumPostCommand request, CancellationToken cancellationToken)
        => _forumService.DeletePostAsync(request.PostId, request.RequestingUserId, cancellationToken);
}
