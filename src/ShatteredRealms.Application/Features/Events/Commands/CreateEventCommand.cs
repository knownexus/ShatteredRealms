using MediatR;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Commands;

public sealed record CreateEventCommand(
    string CreatedById,
    string Title,
    string Description,
    DateTime StartsAt,
    DateTime EndsAt,
    string? Location,
    int? MemberCap
) : IRequest<Result<EventDto>>;
