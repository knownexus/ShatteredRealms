using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class CancelRegistrationCommandHandler : IRequestHandler<CancelRegistrationCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public CancelRegistrationCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(CancelRegistrationCommand request, CancellationToken cancellationToken)
    {
        var attendee = await _context.EventAttendee
            .FirstOrDefaultAsync(a => a.EventId == request.EventId && a.UserId == request.UserId, cancellationToken);

        var ev = await _context.Event.FindAsync(request.EventId, cancellationToken);
        
        if (attendee is null)
        {
            return Result.Failure(DomainErrors.Event.NotRegistered);
        }

        if (ev?.EndsAt < DateTime.UtcNow)
        {
            return Result.Failure(DomainErrors.Event.Ended);
        }

        _context.EventAttendee.Remove(attendee);
        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryActionType.EventRegistrationCancelled, request.UserId, string.Empty,
            targetId: request.EventId.ToString(), cancellationToken: cancellationToken);

        return Result.Success();
    }
}
