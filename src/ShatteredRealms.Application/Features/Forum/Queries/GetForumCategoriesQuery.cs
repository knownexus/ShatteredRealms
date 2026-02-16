using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Queries;

/// <summary>Returns all forum categories ordered by SortOrder.</summary>
public sealed record GetForumCategoriesQuery : IRequest<Result<List<ForumCategoryDto>>>;
