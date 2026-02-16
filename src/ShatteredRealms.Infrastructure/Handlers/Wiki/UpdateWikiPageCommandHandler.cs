using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Wiki;

public sealed class UpdateWikiPageCommandHandler
    : IRequestHandler<UpdateWikiPageCommand, Result<WikiPageDto>>
{
    private readonly IWikiService _wikiService;
    public UpdateWikiPageCommandHandler(IWikiService wikiService) => _wikiService = wikiService;

    public Task<Result<WikiPageDto>> Handle(UpdateWikiPageCommand request, CancellationToken cancellationToken)
    {
        return _wikiService.UpdatePageAsync(request.PageId,
                                            request.RequestingUserId,
                                            new UpdateWikiPageRequest(request.Title, request.Content
                                                                    , request.RevisionNote, request.CategoryIds),
                                            cancellationToken);
    }
}
