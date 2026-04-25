using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record AssignExperienceCommand(
    int CharacterId,
    int XpToAdd
) : IRequest<Result<CharacterDto>>;
