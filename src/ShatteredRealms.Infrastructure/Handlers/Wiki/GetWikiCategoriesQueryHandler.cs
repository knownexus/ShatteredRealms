using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class GetWikiCategoriesQueryHandler
    : IRequestHandler<GetWikiCategoriesQuery, Result<WikiCategoriesDto>>
{
    private readonly IWikiService _wikiService;
    public GetWikiCategoriesQueryHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiCategoriesDto>> Handle(
        GetWikiCategoriesQuery request, CancellationToken cancellationToken)
        => _wikiService.GetCategoriesAsync(cancellationToken);
}
