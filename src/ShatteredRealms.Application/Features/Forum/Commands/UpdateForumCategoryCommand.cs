using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Updates an existing forum category. Admin only.</summary>
public sealed record UpdateForumCategoryCommand(
    int    CategoryId,
    string Name,
    string Description,
    int    SortOrder) : IRequest<Result<ForumCategoryDto>>;
