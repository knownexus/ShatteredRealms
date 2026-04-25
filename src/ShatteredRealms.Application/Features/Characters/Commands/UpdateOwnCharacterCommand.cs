using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record UpdateOwnCharacterCommand(
    int CharacterId,
    string RequestingUserId,
    string Name,
    string Nationality
) : IRequest<Result<CharacterDto>>;
