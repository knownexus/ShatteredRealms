using MediatR;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Analytics.Queries;

public record GetActivityChartQuery(
    DateTime From,
    DateTime To,
    string GroupBy = "day",
    TelemetryActionType? EventType = null,
    string? ActorSearch = null
) : IRequest<Result<ActivityChartDto>>;
