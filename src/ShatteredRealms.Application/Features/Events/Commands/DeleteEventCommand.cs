using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Commands;

public sealed record DeleteEventCommand(int Id, string? ActorId = null) : IRequest<Result>;
