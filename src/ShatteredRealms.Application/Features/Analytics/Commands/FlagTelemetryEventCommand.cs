using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Analytics.Commands;

public record FlagTelemetryEventCommand(Guid EventId, string ActorId, string? Reason = null) : IRequest<Result>;

public record UnflagTelemetryEventCommand(Guid EventId) : IRequest<Result>;
