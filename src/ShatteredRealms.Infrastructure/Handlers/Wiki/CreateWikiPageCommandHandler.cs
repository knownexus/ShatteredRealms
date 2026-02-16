using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class CreateWikiPageCommandHandler
    : IRequestHandler<CreateWikiPageCommand, Result<WikiPageDto>>
{
    private readonly IWikiService _wikiService;
    public CreateWikiPageCommandHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiPageDto>> Handle(
        CreateWikiPageCommand request, CancellationToken cancellationToken)
        => _wikiService.CreatePageAsync(
            request.RequestingUserId,
            new CreateWikiPageRequest(request.Title, request.Content, request.RevisionNote, request.CategoryIds),
            cancellationToken);
}
