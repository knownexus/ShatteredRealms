using MediatR;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Queries;

public sealed record GetAllEventsQuery(string? CurrentUserId, bool UpcomingOnly = true) : IRequest<Result<List<EventDto>>>;
