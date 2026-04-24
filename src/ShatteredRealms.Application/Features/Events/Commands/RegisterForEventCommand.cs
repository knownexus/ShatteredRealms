using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Commands;

public sealed record RegisterForEventCommand(int EventId, string UserId) : IRequest<Result>;
