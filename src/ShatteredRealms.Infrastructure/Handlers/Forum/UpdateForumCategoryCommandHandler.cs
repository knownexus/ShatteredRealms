using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class UpdateForumCategoryCommandHandler
    : IRequestHandler<UpdateForumCategoryCommand, Result<ForumCategoryDto>>
{
    private readonly IForumService _forumService;
    public UpdateForumCategoryCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumCategoryDto>> Handle(
        UpdateForumCategoryCommand request, CancellationToken cancellationToken)
        => _forumService.UpdateCategoryAsync(
            request.CategoryId,
            new UpdateForumCategoryRequest(request.Name, request.Description, request.SortOrder),
            cancellationToken);
}
