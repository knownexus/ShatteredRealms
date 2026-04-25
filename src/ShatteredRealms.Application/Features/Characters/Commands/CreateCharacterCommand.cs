using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record CreateCharacterCommand(
    string UserId,
    string Name,
    string Nationality
) : IRequest<Result<CharacterDto>>;
