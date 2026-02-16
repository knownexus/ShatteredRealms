using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class GetWikiPagesQueryHandler
    : IRequestHandler<GetWikiPagesQuery, Result<WikiPagesDto>>
{
    private readonly IWikiService _wikiService;
    public GetWikiPagesQueryHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiPagesDto>> Handle(
        GetWikiPagesQuery request, CancellationToken cancellationToken)
        => _wikiService.GetPagesAsync(request.CategoryId, cancellationToken);
}
