using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Queries;

/// <summary>Returns a single wiki page by its integer ID.</summary>
public sealed record GetWikiPageQuery(int PageId) : IRequest<Result<WikiPageDto>>;
