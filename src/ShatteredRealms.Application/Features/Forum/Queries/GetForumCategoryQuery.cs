using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Queries;

/// <summary>Returns a single forum category by ID.</summary>
public sealed record GetForumCategoryQuery(int CategoryId) : IRequest<Result<ForumCategoryDto>>;
