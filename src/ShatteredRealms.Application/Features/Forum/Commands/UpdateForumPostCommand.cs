using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Edits a post's content. The requesting user must be the post's author.</summary>
public sealed record UpdateForumPostCommand(
    int    PostId,
    string RequestingUserId,
    string Content) : IRequest<Result<ForumPostDto>>;
