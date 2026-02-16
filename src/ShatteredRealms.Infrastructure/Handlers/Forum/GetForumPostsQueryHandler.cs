using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class GetForumPostsQueryHandler
    : IRequestHandler<GetForumPostsQuery, Result<List<ForumPostDto>>>
{
    private readonly IForumService _forumService;
    public GetForumPostsQueryHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<List<ForumPostDto>>> Handle(
        GetForumPostsQuery request, CancellationToken cancellationToken)
        => _forumService.GetPostsFromThreadAsync(request.ThreadId, cancellationToken);
}
