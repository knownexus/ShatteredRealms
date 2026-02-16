using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class CreateForumCategoryCommandHandler
    : IRequestHandler<CreateForumCategoryCommand, Result<ForumCategoryDto>>
{
    private readonly IForumService _forumService;
    public CreateForumCategoryCommandHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumCategoryDto>> Handle(
        CreateForumCategoryCommand request, CancellationToken cancellationToken)
        => _forumService.CreateCategoryAsync(
            request.RequestingUserId,
            new CreateForumCategoryRequest(request.Name, request.Description, request.SortOrder),
            cancellationToken);
}
