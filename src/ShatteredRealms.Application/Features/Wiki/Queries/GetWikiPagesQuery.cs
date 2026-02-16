using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Queries;

/// <summary>Returns all non-deleted wiki pages, optionally filtered by category.</summary>
public sealed record GetWikiPagesQuery(int? CategoryId = null) : IRequest<Result<WikiPagesDto>>;
