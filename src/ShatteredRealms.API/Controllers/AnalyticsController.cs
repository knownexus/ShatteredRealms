using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Commands;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class AnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnalyticsController(IMediator mediator) => _mediator = mediator;

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpGet]
    public async Task<ActionResult<PagedTelemetryResult>> GetEvents(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] TelemetryEventType? eventType = null,
        [FromQuery] string? actorSearch = null,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetTelemetryEventsQuery(page, pageSize, eventType, actorSearch, from, to),
            cancellationToken);

        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpGet("summary")]
    public async Task<ActionResult<AnalyticsSummaryDto>> GetSummary(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAnalyticsSummaryQuery(), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpPut("{id:guid}/flag")]
    public async Task<IActionResult> Flag(Guid id, [FromBody] FlagEventRequest request, CancellationToken cancellationToken)
    {
        var actorId = User.GetUserId();
        if (string.IsNullOrEmpty(actorId))
            return Problem(detail: "User ID cannot be resolved", statusCode: 400, title: "Invalid User");

        var result = await _mediator.Send(new FlagTelemetryEventCommand(id, actorId, request.Reason), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpDelete("{id:guid}/flag")]
    public async Task<IActionResult> Unflag(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnflagTelemetryEventCommand(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpGet("rules")]
    public async Task<ActionResult<List<AnalyticsFlagRuleDto>>> GetRules(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetFlagRulesQuery(), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpPost("rules")]
    public async Task<ActionResult<AnalyticsFlagRuleDto>> CreateRule(
        [FromBody] CreateFlagRuleRequest request,
        CancellationToken cancellationToken)
    {
        var actorId = User.GetUserId();
        if (string.IsNullOrEmpty(actorId))
            return Problem(detail: "User ID cannot be resolved", statusCode: 400, title: "Invalid User");

        var result = await _mediator.Send(
            new CreateFlagRuleCommand(request.RuleType, request.TargetUserId, request.EventType, request.ActorRole, request.Reason, actorId),
            cancellationToken);

        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpDelete("rules/{id:int}")]
    public async Task<IActionResult> DeleteRule(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteFlagRuleCommand(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpPatch("rules/{id:int}/active")]
    public async Task<IActionResult> SetRuleActive(int id, [FromBody] SetRuleActiveRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetFlagRuleActiveCommand(id, request.IsActive), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpGet("chart")]
    public async Task<ActionResult<ActivityChartDto>> GetChart(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        [FromQuery] string groupBy = "day",
        [FromQuery] TelemetryEventType? eventType = null,
        [FromQuery] string? actorSearch = null,
        CancellationToken cancellationToken = default)
    {
        var effectiveFrom = from ?? DateTime.UtcNow.AddDays(-30);
        var effectiveTo   = to   ?? DateTime.UtcNow;
        var result = await _mediator.Send(
            new GetActivityChartQuery(effectiveFrom, effectiveTo, groupBy, eventType, actorSearch),
            cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Analytics.View)]
    [HttpGet("event-attendances")]
    public async Task<ActionResult<List<EventAttendanceOverviewDto>>> GetEventAttendances(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetEventAttendanceOverviewQuery(), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }
}

public record FlagEventRequest(string? Reason);
public record CreateFlagRuleRequest(FlagRuleType RuleType, string? TargetUserId, TelemetryEventType? EventType, string? ActorRole, string Reason);
public record SetRuleActiveRequest(bool IsActive);
