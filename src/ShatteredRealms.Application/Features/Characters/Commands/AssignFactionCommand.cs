using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record AssignFactionCommand(
    int CharacterId,
    string Faction,
    string RequestingUserId
) : IRequest<Result<CharacterDto>>;
