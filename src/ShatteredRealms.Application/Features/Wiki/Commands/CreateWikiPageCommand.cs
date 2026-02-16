using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Commands;

/// <summary>Creates a new wiki page with an initial revision.</summary>
public sealed record CreateWikiPageCommand(
    string      RequestingUserId,
    string      Title,
    string      Content,
    string      RevisionNote,
    List<int>   CategoryIds) : IRequest<Result<WikiPageDto>>;
