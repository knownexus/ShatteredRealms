using MediatR;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Analytics.Queries;

public sealed record GetTelemetryEventsQuery(
    int Page = 1,
    int PageSize = 50,
    TelemetryEventType? EventType = null,
    string? ActorId = null,
    DateTime? From = null,
    DateTime? To = null
) : IRequest<Result<PagedTelemetryResult>>;

public sealed class PagedTelemetryResult
{
    public List<TelemetryEventDto> Events { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
