using MediatR;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class DeleteWikiPageCommandHandler : IRequestHandler<DeleteWikiPageCommand, Result>
{
    private readonly IWikiService _wikiService;
    public DeleteWikiPageCommandHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result> Handle(DeleteWikiPageCommand request, CancellationToken cancellationToken)
        => _wikiService.DeletePageAsync(request.PageId, cancellationToken);
}
