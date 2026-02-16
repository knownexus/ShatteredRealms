using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class GetWikiPageBySlugQueryHandler
    : IRequestHandler<GetWikiPageBySlugQuery, Result<WikiPageDto>>
{
    private readonly IWikiService _wikiService;
    public GetWikiPageBySlugQueryHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiPageDto>> Handle(
        GetWikiPageBySlugQuery request, CancellationToken cancellationToken)
        => _wikiService.GetPageBySlugAsync(request.Slug, cancellationToken);
}
