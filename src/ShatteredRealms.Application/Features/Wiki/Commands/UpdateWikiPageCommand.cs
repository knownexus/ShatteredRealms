using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Commands;

/// <summary>Updates a wiki page's content and creates a new revision snapshot.</summary>
public sealed record UpdateWikiPageCommand(
    int         PageId,
    string      RequestingUserId,
    string      Title,
    string      Content,
    string      RevisionNote,
    List<int>   CategoryIds) : IRequest<Result<WikiPageDto>>;
