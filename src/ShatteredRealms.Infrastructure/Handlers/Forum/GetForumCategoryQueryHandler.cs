using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class GetForumCategoryQueryHandler
    : IRequestHandler<GetForumCategoryQuery, Result<ForumCategoryDto>>
{
    private readonly IForumService _forumService;
    public GetForumCategoryQueryHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumCategoryDto>> Handle(
        GetForumCategoryQuery request, CancellationToken cancellationToken)
        => _forumService.GetCategoryAsync(request.CategoryId, cancellationToken);
}
