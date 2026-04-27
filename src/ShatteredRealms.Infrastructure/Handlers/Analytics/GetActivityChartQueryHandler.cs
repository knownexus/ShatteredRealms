using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class GetActivityChartQueryHandler : IRequestHandler<GetActivityChartQuery, Result<ActivityChartDto>>
{
    private readonly ApplicationDbContext _context;

    public GetActivityChartQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<ActivityChartDto>> Handle(GetActivityChartQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TelemetryEvent
            .Where(e => e.OccurredAt >= request.From && e.OccurredAt <= request.To);

        if (request.EventType.HasValue)
            query = query.Where(e => e.EventType == request.EventType.Value);

        if (!string.IsNullOrEmpty(request.ActorSearch))
        {
            var search = request.ActorSearch.ToLower();
            query = query.Where(e =>
                e.ActorName.ToLower().Contains(search) ||
                e.ActorEmail.ToLower().Contains(search) ||
                e.ActorId == request.ActorSearch);
        }

        var rawEvents = await query
            .Select(e => new { e.OccurredAt, e.EventType })
            .ToListAsync(cancellationToken);

        var groupByKey = request.GroupBy.ToLower();
        var grouped = groupByKey switch
        {
            "week"  => rawEvents.GroupBy(e => StartOfWeek(e.OccurredAt)),
            "month" => rawEvents.GroupBy(e => new DateTime(e.OccurredAt.Year, e.OccurredAt.Month, 1)),
            _       => rawEvents.GroupBy(e => e.OccurredAt.Date),
        };

        var dataPoints = grouped
            .Select(g => new ActivityDataPoint(g.Key, g.Count()))
            .OrderBy(p => p.Date)
            .ToList();

        // Fill day-level gaps so bars are evenly spaced (max 91 days to keep it useful)
        if (groupByKey == "day" && (request.To - request.From).TotalDays <= 91)
        {
            var lookup = dataPoints.ToDictionary(p => p.Date.Date);
            var filled = new List<ActivityDataPoint>();
            for (var d = request.From.Date; d <= request.To.Date; d = d.AddDays(1))
                filled.Add(lookup.TryGetValue(d, out var pt) ? pt : new ActivityDataPoint(d, 0));
            dataPoints = filled;
        }

        var breakdown = rawEvents
            .GroupBy(e => e.EventType.ToString())
            .Select(g => new EventTypeBreakdownPoint(g.Key, g.Count()))
            .OrderByDescending(b => b.Count)
            .Take(15)
            .ToList();

        return Result.Success(new ActivityChartDto { DataPoints = dataPoints, Breakdown = breakdown });
    }

    private static DateTime StartOfWeek(DateTime dt)
    {
        var diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
        return dt.Date.AddDays(-diff);
    }
}
