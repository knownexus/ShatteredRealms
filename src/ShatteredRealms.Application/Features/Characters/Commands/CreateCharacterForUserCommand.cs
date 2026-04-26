using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record CreateCharacterForUserCommand(
    string TargetUserId,
    string Name,
    string Nationality,
    string Faction,
    string RequestingUserId
) : IRequest<Result<CharacterDto>>;
