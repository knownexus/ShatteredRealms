using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Creates a new thread with an opening post. Any authenticated user.</summary>
public sealed record CreateForumThreadCommand(
    string RequestingUserId,
    int    CategoryId,
    string Title,
    string InitialPostContent) : IRequest<Result<ForumThreadDto>>;
