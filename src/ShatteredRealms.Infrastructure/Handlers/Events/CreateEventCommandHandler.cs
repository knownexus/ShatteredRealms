using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Event;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<EventDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public CreateEventCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.EndsAt <= request.StartsAt)
        {
            return Result.Failure<EventDto>(DomainErrors.Event.InvalidDates);
        }

        var ev = new Event
        {
            Title       = request.Title,
            Description = request.Description,
            StartsAt    = request.StartsAt,
            EndsAt      = request.EndsAt,
            Location    = request.Location,
            MemberCap   = request.MemberCap,
            CreatedById = request.CreatedById,
            CreatedAt   = DateTime.UtcNow,
        };

        _context.Event.Add(ev);
        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryActionType.EventCreated, request.CreatedById, string.Empty,
            targetId: ev.Id.ToString(), targetName: ev.Title, cancellationToken: cancellationToken);

        return Result.Success(new EventDto
        {
            Id            = ev.Id,
            Title         = ev.Title,
            Description   = ev.Description,
            StartsAt      = ev.StartsAt,
            EndsAt        = ev.EndsAt,
            Location      = ev.Location,
            BannerImagePath = ev.BannerImagePath,
            MemberCap     = ev.MemberCap,
            CreatedById   = ev.CreatedById,
            CreatedAt     = ev.CreatedAt,
            AttendeeCount = 0,
            IsGoing       = false,
        });
    }
}
