using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Updates thread metadata (title, pinned, locked). Admin/moderator only.</summary>
public sealed record UpdateForumThreadCommand(
    int    ThreadId,
    string RequestingUserId,
    string Title,
    bool   IsPinned,
    bool   IsLocked) : IRequest<Result<ForumThreadDto>>;
