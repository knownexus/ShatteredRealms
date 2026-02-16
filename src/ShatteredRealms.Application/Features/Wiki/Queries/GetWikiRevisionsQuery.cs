using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Queries;

/// <summary>Returns all revisions for a page, most recent first.</summary>
public sealed record GetWikiRevisionsQuery(int PageId) : IRequest<Result<WikiRevisionsDto>>;
