using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class GetFlagRulesQueryHandler : IRequestHandler<GetFlagRulesQuery, Result<List<AnalyticsFlagRuleDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetFlagRulesQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<AnalyticsFlagRuleDto>>> Handle(GetFlagRulesQuery request, CancellationToken cancellationToken)
    {
        var rules = await _context.AnalyticsFlagRule
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        var userIds = rules
            .SelectMany(r => new[] { r.CreatedById, r.TargetUserId })
            .Where(id => id is not null)
            .Distinct()
            .ToList();

        var users = await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .Select(u => new { u.Id, u.UserName })
            .ToDictionaryAsync(u => u.Id, u => u.UserName ?? u.Id, cancellationToken);

        var dtos = rules.Select(r => new AnalyticsFlagRuleDto
        {
            Id             = r.Id,
            RuleType       = r.RuleType,
            TargetUserId   = r.TargetUserId,
            TargetUserName = r.TargetUserId is not null && users.TryGetValue(r.TargetUserId, out var tu) ? tu : r.TargetUserId,
            EventType      = r.EventType,
            ActorRole      = r.ActorRole,
            Reason         = r.Reason,
            CreatedById    = r.CreatedById,
            CreatedByName  = users.TryGetValue(r.CreatedById, out var cu) ? cu : r.CreatedById,
            CreatedAt      = r.CreatedAt,
            IsActive       = r.IsActive,
        }).ToList();

        return Result.Success(dtos);
    }
}
