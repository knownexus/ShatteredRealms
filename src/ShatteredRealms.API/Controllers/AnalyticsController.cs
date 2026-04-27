using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.Application.DTOs.Analytics;
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
        [FromQuery] string? actorId = null,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetTelemetryEventsQuery(page, pageSize, eventType, actorId, from, to),
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
}
