using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumThreadCommandHandler : IRequestHandler<DeleteForumThreadCommand, Result>
{
    private readonly IForumService _forumService;
    public DeleteForumThreadCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result> Handle(DeleteForumThreadCommand request, CancellationToken cancellationToken)
        => _forumService.DeleteThreadAsync(request.ThreadId, cancellationToken);
}
