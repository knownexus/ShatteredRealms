using MediatR;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Queries;

public record GetEventAttendeesQuery(int EventId) : IRequest<Result<List<EventAttendeeDto>>>;
