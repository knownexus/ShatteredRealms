using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class GetForumCategoriesQueryHandler
    : IRequestHandler<GetForumCategoriesQuery, Result<List<ForumCategoryDto>>>
{
    private readonly IForumService _forumService;
    public GetForumCategoriesQueryHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<List<ForumCategoryDto>>> Handle(
        GetForumCategoriesQuery request, CancellationToken cancellationToken)
        => _forumService.GetCategoriesAsync(cancellationToken);
}
