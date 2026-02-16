using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Adds a reply to an unlocked thread. Any authenticated user.</summary>
public sealed record CreateForumPostCommand(
    string RequestingUserId,
    int    ThreadId,
    string Content) : IRequest<Result<ForumPostDto>>;
