using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Queries;

/// <summary>Returns all pages for a category as DTOs.</summary>
public sealed record GetPagesByCategoryIdQuery(int? CategoryId = null) : IRequest<Result<WikiPagesDto>>;
