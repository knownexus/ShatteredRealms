using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Event;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class RegisterForEventCommandHandler : IRequestHandler<RegisterForEventCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public RegisterForEventCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(RegisterForEventCommand request, CancellationToken cancellationToken)
    {
        var ev = await _context.Event
            .Include(e => e.Attendees)
            .FirstOrDefaultAsync(e => e.Id == request.EventId, cancellationToken);

        if (ev is null)
            return Result.Failure(DomainErrors.Event.NotFound);

        if (ev.Attendees.Any(a => a.UserId == request.UserId))
            return Result.Success(); // idempotent

        if (ev.MemberCap.HasValue && ev.Attendees.Count >= ev.MemberCap.Value)
            return Result.Failure(DomainErrors.Event.CapacityReached);

        _context.EventAttendee.Add(new EventAttendee
        {
            EventId      = request.EventId,
            UserId       = request.UserId,
            RegisteredAt = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryEventType.EventRegistered, request.UserId, string.Empty,
            targetId: request.EventId.ToString(), targetName: ev.Title, cancellationToken: cancellationToken);

        return Result.Success();
    }
}
