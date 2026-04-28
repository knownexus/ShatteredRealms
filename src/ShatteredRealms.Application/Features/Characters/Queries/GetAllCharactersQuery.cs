using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Queries;

public sealed record GetAllCharactersQuery(
    string? Search = null,
    string? UserId = null,
    string? UserName = null,
    string? Role = null,
    string? Nation = null
) : IRequest<Result<List<CharacterDto>>>;
