using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Forum;

public sealed class GetForumPostQueryHandler
    : IRequestHandler<GetForumPostQuery, Result<ForumPostDto>>
{
    private readonly IForumService _forumService;
    public GetForumPostQueryHandler(IForumService forumService) => _forumService = forumService;

    public Task<Result<ForumPostDto>> Handle(
        GetForumPostQuery request, CancellationToken cancellationToken)
        => _forumService.GetPostAsync(request.PostId, cancellationToken);
}
