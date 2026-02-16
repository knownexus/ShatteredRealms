using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Creates a new forum category. Admin only.</summary>
public sealed record CreateForumCategoryCommand(
    string RequestingUserId,
    string Name,
    string Description,
    int    SortOrder) : IRequest<Result<ForumCategoryDto>>;
