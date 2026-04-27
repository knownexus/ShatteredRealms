using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class GetTelemetryEventsQueryHandler : IRequestHandler<GetTelemetryEventsQuery, Result<PagedTelemetryResult>>
{
    private readonly ApplicationDbContext _context;

    public GetTelemetryEventsQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<PagedTelemetryResult>> Handle(GetTelemetryEventsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TelemetryEvent.AsQueryable();

        if (request.EventType.HasValue)
            query = query.Where(e => e.EventType == request.EventType.Value);

        if (!string.IsNullOrEmpty(request.ActorId))
            query = query.Where(e => e.ActorId == request.ActorId);

        if (request.From.HasValue)
            query = query.Where(e => e.OccurredAt >= request.From.Value);

        if (request.To.HasValue)
            query = query.Where(e => e.OccurredAt <= request.To.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var page     = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 200);

        var events = await query
            .OrderByDescending(e => e.OccurredAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new TelemetryEventDto
            {
                Id          = e.Id,
                EventType   = e.EventType,
                ActorId     = e.ActorId,
                ActorEmail  = e.ActorEmail,
                TargetId    = e.TargetId,
                TargetName  = e.TargetName,
                Details     = e.Details,
                OccurredAt  = e.OccurredAt,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(new PagedTelemetryResult
        {
            Events     = events,
            TotalCount = totalCount,
            Page       = page,
            PageSize   = pageSize,
        });
    }
}
