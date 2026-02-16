using MediatR;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class DeleteForumCategoryCommandHandler : IRequestHandler<DeleteForumCategoryCommand, Result>
{
    private readonly IForumService _forumService;
    public DeleteForumCategoryCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result> Handle(DeleteForumCategoryCommand request, CancellationToken cancellationToken)
        => _forumService.DeleteCategoryAsync(request.CategoryId, cancellationToken);
}
