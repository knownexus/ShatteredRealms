using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class GetWikiPageQueryHandler
    : IRequestHandler<GetWikiPageQuery, Result<WikiPageDto>>
{
    private readonly IWikiService _wikiService;
    public GetWikiPageQueryHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiPageDto>> Handle(
        GetWikiPageQuery request, CancellationToken cancellationToken)
        => _wikiService.GetPageAsync(request.PageId, cancellationToken);
}
