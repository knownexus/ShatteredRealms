using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class GetForumThreadQueryHandler
    : IRequestHandler<GetForumThreadQuery, Result<ForumThreadDto>>
{
    private readonly IForumService _forumService;
    public GetForumThreadQueryHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumThreadDto>> Handle(
        GetForumThreadQuery request, CancellationToken cancellationToken)
        => _forumService.GetThreadAsync(request.ThreadId, cancellationToken);
}
