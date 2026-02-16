using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class GetWikiRevisionsQueryHandler
    : IRequestHandler<GetWikiRevisionsQuery, Result<WikiRevisionsDto>>
{
    private readonly IWikiService _wikiService;
    public GetWikiRevisionsQueryHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiRevisionsDto>> Handle(
        GetWikiRevisionsQuery request, CancellationToken cancellationToken)
        => _wikiService.GetRevisionsAsync(request.PageId, cancellationToken);
}
