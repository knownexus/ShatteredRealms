using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record AssignPositionCommand(
    int CharacterId,
    int? PositionId,
    string RequestingUserId
) : IRequest<Result<CharacterDto>>;
