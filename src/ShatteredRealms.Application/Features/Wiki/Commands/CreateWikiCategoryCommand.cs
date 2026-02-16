using MediatR;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Commands;

/// <summary>Creates a new wiki category. Admin only.</summary>
public sealed record CreateWikiCategoryCommand(
    string Name,
    string Description) : IRequest<Result<WikiCategoryDto>>;
