using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumPostAsAdminCommandHandler : IRequestHandler<DeleteForumPostAsAdminCommand, Result>
{
    private readonly IForumService _forumService;
    public DeleteForumPostAsAdminCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result> Handle(DeleteForumPostAsAdminCommand request, CancellationToken cancellationToken)
        => _forumService.DeletePostAsAdminAsync(request.PostId, cancellationToken);
}
