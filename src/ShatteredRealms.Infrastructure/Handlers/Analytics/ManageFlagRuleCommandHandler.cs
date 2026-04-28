using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Commands;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class CreateFlagRuleCommandHandler : IRequestHandler<CreateFlagRuleCommand, Result<AnalyticsFlagRuleDto>>
{
    private readonly ApplicationDbContext _context;

    public CreateFlagRuleCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<AnalyticsFlagRuleDto>> Handle(CreateFlagRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = new AnalyticsFlagRule
        {
            RuleType      = request.RuleType,
            TargetUserId  = request.TargetUserId,
            EventType     = request.EventType,
            ActorRole     = request.ActorRole,
            Reason        = request.Reason,
            CreatedById   = request.CreatedById,
            CreatedAt     = DateTime.UtcNow,
            IsActive      = true,
        };

        _context.AnalyticsFlagRule.Add(rule);
        await _context.SaveChangesAsync(cancellationToken);

        var createdBy = await _context.Users.FindAsync([request.CreatedById], cancellationToken);
        var targetUser = request.TargetUserId is not null
            ? await _context.Users.FindAsync([request.TargetUserId], cancellationToken)
            : null;

        return Result.Success(MapToDto(rule, createdBy?.UserName, targetUser?.UserName));
    }

    private static AnalyticsFlagRuleDto MapToDto(AnalyticsFlagRule rule, string? createdByName, string? targetUserName) =>
        new()
        {
            Id             = rule.Id,
            RuleType       = rule.RuleType,
            TargetUserId   = rule.TargetUserId,
            TargetUserName = targetUserName,
            EventType      = rule.EventType,
            ActorRole      = rule.ActorRole,
            Reason         = rule.Reason,
            CreatedById    = rule.CreatedById,
            CreatedByName  = createdByName ?? rule.CreatedById,
            CreatedAt      = rule.CreatedAt,
            IsActive       = rule.IsActive,
        };
}

public sealed class DeleteFlagRuleCommandHandler : IRequestHandler<DeleteFlagRuleCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteFlagRuleCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteFlagRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = await _context.AnalyticsFlagRule.FindAsync([request.RuleId], cancellationToken);
        if (rule is null)
        {
            return Result.Failure(DomainErrors.Analytics.FlagRuleNotFound);
        }

        _context.AnalyticsFlagRule.Remove(rule);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

public sealed class SetFlagRuleActiveCommandHandler : IRequestHandler<SetFlagRuleActiveCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public SetFlagRuleActiveCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(SetFlagRuleActiveCommand request, CancellationToken cancellationToken)
    {
        var rule = await _context.AnalyticsFlagRule.FindAsync([request.RuleId], cancellationToken);
        if (rule is null)
        {
            return Result.Failure(DomainErrors.Analytics.FlagRuleNotFound);
        }

        rule.IsActive = request.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
