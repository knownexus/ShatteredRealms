using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class GetPagesByCategoryIdHandler(IWikiService wikiService) : IRequestHandler<GetPagesByCategoryIdQuery, Result<WikiPagesDto>>
{
    public async Task<Result<WikiPagesDto>> Handle(GetPagesByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var pagesResult = await wikiService.GetPagesAsync(request.CategoryId, cancellationToken);
        if (pagesResult.IsFailure)
        {
            return Result.Failure<WikiPagesDto>(pagesResult.Error);
        }

        return Result.Success(pagesResult.Value);
    }
}
