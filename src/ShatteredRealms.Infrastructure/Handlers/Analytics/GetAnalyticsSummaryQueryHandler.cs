using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class GetAnalyticsSummaryQueryHandler : IRequestHandler<GetAnalyticsSummaryQuery, Result<AnalyticsSummaryDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAnalyticsSummaryQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<AnalyticsSummaryDto>> Handle(GetAnalyticsSummaryQuery request, CancellationToken cancellationToken)
    {
        var now       = DateTime.UtcNow;
        var todayStart = now.Date;
        var weekStart  = todayStart.AddDays(-(int)now.DayOfWeek);
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var allEvents = _context.TelemetryEvent.AsQueryable();

        var todayCount = await allEvents.CountAsync(e => e.OccurredAt >= todayStart, cancellationToken);
        var weekCount  = await allEvents.CountAsync(e => e.OccurredAt >= weekStart, cancellationToken);
        var monthCount = await allEvents.CountAsync(e => e.OccurredAt >= monthStart, cancellationToken);

        var uniqueActors = await allEvents
            .Where(e => e.OccurredAt >= weekStart)
            .Select(e => e.ActorId)
            .Distinct()
            .CountAsync(cancellationToken);

        var loginsToday = await allEvents
            .CountAsync(e => e.ActionType == TelemetryActionType.UserLoggedIn && e.OccurredAt >= todayStart, cancellationToken);

        var loginsWeek = await allEvents
            .CountAsync(e => e.ActionType == TelemetryActionType.UserLoggedIn && e.OccurredAt >= weekStart, cancellationToken);

        var topTypes = await allEvents
            .Where(e => e.OccurredAt >= weekStart)
            .GroupBy(e => e.ActionType)
            .Select(g => new EventTypeCount { ActionType = g.Key.ToString(), Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToListAsync(cancellationToken);

        var recent = await allEvents
            .OrderByDescending(e => e.OccurredAt)
            .Take(20)
            .Select(e => new TelemetryActionDto
            {
                Id          = e.Id,
                ActionType   = e.ActionType,
                ActorId     = e.ActorId,
                ActorName   = e.ActorName,
                ActorEmail  = e.ActorEmail,
                ActorRole   = e.ActorRole,
                TargetId    = e.TargetId,
                TargetName  = e.TargetName,
                Details     = e.Details,
                OccurredAt  = e.OccurredAt,
                IsFlagged   = e.IsFlagged,
                FlagReason  = e.FlagReason,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(new AnalyticsSummaryDto
        {
            TotalActionsToday      = todayCount,
            TotalActionsThisWeek   = weekCount,
            TotalActionsThisMonth  = monthCount,
            UniqueUsersThisWeek  = uniqueActors,
            LoginsToday           = loginsToday,
            LoginsThisWeek        = loginsWeek,
            TopActionTypes         = topTypes,
            RecentEvents          = recent,
        });
    }
}
