using MediatR;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Queries;

public sealed record GetEventByIdQuery(int Id, string? CurrentUserId) : IRequest<Result<EventDto>>;
