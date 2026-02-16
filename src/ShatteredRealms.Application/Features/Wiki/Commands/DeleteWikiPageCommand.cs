using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Commands;

/// <summary>Soft-deletes a wiki page. Admin only.</summary>
public sealed record DeleteWikiPageCommand(int PageId) : IRequest<Result>;
