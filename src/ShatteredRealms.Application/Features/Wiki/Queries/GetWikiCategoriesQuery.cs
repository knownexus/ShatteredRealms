using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Queries;

/// <summary>Returns all wiki categories ordered alphabetically.</summary>
public sealed record GetWikiCategoriesQuery : IRequest<Result<WikiCategoriesDto>>;
