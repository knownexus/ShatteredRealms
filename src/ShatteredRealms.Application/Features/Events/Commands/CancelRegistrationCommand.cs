using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Commands;

public sealed record CancelRegistrationCommand(int EventId, string UserId) : IRequest<Result>;
