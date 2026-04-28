using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class GetEventAttendanceOverviewQueryHandler : IRequestHandler<GetEventAttendanceOverviewQuery, Result<List<EventAttendanceOverviewDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetEventAttendanceOverviewQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<EventAttendanceOverviewDto>>> Handle(GetEventAttendanceOverviewQuery request, CancellationToken cancellationToken)
    {
        var events = await _context.Event
            .IgnoreQueryFilters()
            .Include(e => e.Attendees)
                .ThenInclude(a => a.User)
            .OrderByDescending(e => e.StartsAt)
            .ToListAsync(cancellationToken);

        var cancellations = await _context.TelemetryEvent
            .Where(t => t.ActionType == TelemetryActionType.EventRegistrationCancelled)
            .Select(t => new { t.ActorId, t.ActorName, t.ActorEmail, t.TargetId, t.OccurredAt })
            .ToListAsync(cancellationToken);

        var result = events.Select(ev =>
        {
            var currentAttendeeIds = ev.Attendees.Select(a => a.UserId).ToHashSet();

            var currentAttendees = ev.Attendees
                .Select(a => new AttendeeDetailDto
                {
                    UserId      = a.UserId,
                    UserName    = a.User?.UserName ?? a.UserId,
                    UserEmail   = a.User?.Email ?? string.Empty,
                    RegisteredAt = a.RegisteredAt,
                })
                .OrderBy(a => a.RegisteredAt)
                .ToList();

            // Show cancellations for users who are NOT currently registered
            var eventCancellations = cancellations
                .Where(c => c.TargetId == ev.Id.ToString() && !currentAttendeeIds.Contains(c.ActorId))
                .GroupBy(c => c.ActorId)
                .Select(g => g.OrderByDescending(c => c.OccurredAt).First())
                .Select(c => new CancellationDetailDto
                {
                    UserId     = c.ActorId,
                    UserName   = c.ActorName,
                    UserEmail  = c.ActorEmail,
                    CancelledAt = c.OccurredAt,
                })
                .OrderByDescending(c => c.CancelledAt)
                .ToList();

            return new EventAttendanceOverviewDto
            {
                EventId          = ev.Id,
                Title            = ev.Title,
                StartsAt         = ev.StartsAt,
                IsDeleted        = ev.IsDeleted,
                CurrentAttendees = currentAttendees,
                Cancellations    = eventCancellations,
            };
        }).ToList();

        return Result.Success(result);
    }
}
