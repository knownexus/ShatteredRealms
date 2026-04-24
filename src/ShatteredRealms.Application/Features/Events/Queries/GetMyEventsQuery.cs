using MediatR;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Queries;

public sealed record GetMyEventsQuery(string UserId) : IRequest<Result<List<EventDto>>>;
