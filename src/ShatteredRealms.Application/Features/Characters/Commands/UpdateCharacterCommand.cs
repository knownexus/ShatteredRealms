using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record UpdateCharacterCommand(
    int CharacterId,
    string Name,
    string Nationality,
    int Level,
    int Experience
) : IRequest<Result<CharacterDto>>;
