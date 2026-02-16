using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class CreateWikiCategoryCommandHandler
    : IRequestHandler<CreateWikiCategoryCommand, Result<WikiCategoryDto>>
{
    private readonly IWikiService _wikiService;
    public CreateWikiCategoryCommandHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiCategoryDto>> Handle(
        CreateWikiCategoryCommand request, CancellationToken cancellationToken)
        => _wikiService.CreateCategoryAsync(
            new CreateWikiCategoryRequest(request.Name, request.Description),
            cancellationToken);
}
