using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Queries;

/// <summary>Returns a single wiki page by its URL slug.</summary>
public sealed record GetWikiPageBySlugQuery(string Slug) : IRequest<Result<WikiPageDto>>;
