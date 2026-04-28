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
        {
            query = query.Where(e => e.ActionType == request.EventType.Value);
        }

        if (!string.IsNullOrEmpty(request.ActorSearch))
        {
            var search = request.ActorSearch.ToLower();
            query = query.Where(e =>
                e.ActorName.ToLower().Contains(search) ||
                e.ActorEmail.ToLower().Contains(search) ||
                e.ActorId == request.ActorSearch);
        }

        if (request.From.HasValue)
        {
            query = query.Where(e => e.OccurredAt >= request.From.Value);
        }

        if (request.To.HasValue)
        {
            query = query.Where(e => e.OccurredAt <= request.To.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var page     = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 200);

        var events = await query
            .OrderByDescending(e => e.OccurredAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
                FlaggedAt   = e.FlaggedAt,
                FlaggedById = e.FlaggedById,
            })
            .ToListAsync(cancellationToken);

        // Apply active flag rules
        var activeRules = await _context.AnalyticsFlagRule
            .Where(r => r.IsActive)
            .ToListAsync(cancellationToken);

        if (activeRules.Count > 0)
        {
            foreach (var ev in events)
            {
                if (ev.IsFlagged)
                {
                    continue;
                }

                foreach (var rule in activeRules)
                {
                    var matched = rule.RuleType switch
                    {
                        Domain.Entities.Telemetry.FlagRuleType.User =>
                            rule.TargetUserId == ev.ActorId,
                        Domain.Entities.Telemetry.FlagRuleType.ActionType =>
                            !rule.EventType.HasValue || rule.EventType.Value == ev.ActionType,
                        Domain.Entities.Telemetry.FlagRuleType.RoleAction =>
                            !string.IsNullOrEmpty(rule.ActorRole) &&
                            string.Equals(rule.ActorRole, ev.ActorRole, StringComparison.OrdinalIgnoreCase) &&
                            (!rule.EventType.HasValue || rule.EventType.Value == ev.ActionType),
                        _ => false,
                    };

                    if (matched)
                    {
                        ev.IsRuleFlagged     = true;
                        ev.MatchedRuleReason = rule.Reason;
                        break;
                    }
                }
            }
        }

        return Result.Success(new PagedTelemetryResult
        {
            Actions     = events,
            TotalCount = totalCount,
            Page       = page,
            PageSize   = pageSize,
        });
    }
}
