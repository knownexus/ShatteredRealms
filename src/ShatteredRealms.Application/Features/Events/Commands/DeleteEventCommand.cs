using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Commands;

public sealed record DeleteEventCommand(int Id) : IRequest<Result>;
