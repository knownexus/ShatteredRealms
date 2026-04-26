using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Commands;

public sealed record DeleteCharacterCommand(int CharacterId, string RequestingUserId) : IRequest<Result>;
