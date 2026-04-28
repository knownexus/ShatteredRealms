using MediatR;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Analytics.Commands;

public record CreateFlagRuleCommand(
    FlagRuleType RuleType,
    string? TargetUserId,
    TelemetryActionType? EventType,
    string? ActorRole,
    string Reason,
    string CreatedById
) : IRequest<Result<AnalyticsFlagRuleDto>>;

public record DeleteFlagRuleCommand(int RuleId) : IRequest<Result>;

public record SetFlagRuleActiveCommand(int RuleId, bool IsActive) : IRequest<Result>;
