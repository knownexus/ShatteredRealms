using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class GetForumThreadsQueryHandler
    : IRequestHandler<GetForumThreadsQuery, Result<List<ForumThreadDto>>>
{
    private readonly IForumService _forumService;
    public GetForumThreadsQueryHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<List<ForumThreadDto>>> Handle(
        GetForumThreadsQuery request, CancellationToken cancellationToken)
        => _forumService.GetThreadsAsync(request.CategoryId, cancellationToken);
}
