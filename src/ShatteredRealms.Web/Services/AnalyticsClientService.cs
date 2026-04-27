using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Analytics;
using ShatteredRealms.Application.Features.Analytics.Queries;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class AnalyticsClientService
{
    private readonly HttpClient _httpClient;

    public AnalyticsClientService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<Result<PagedTelemetryResult>> GetEventsAsync(
        int page = 1,
        int pageSize = 50,
        TelemetryEventType? eventType = null,
        string? actorId = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var query = $"api/analytics?page={page}&pageSize={pageSize}";
        if (eventType.HasValue)  query += $"&eventType={(int)eventType.Value}";
        if (!string.IsNullOrEmpty(actorId)) query += $"&actorId={Uri.EscapeDataString(actorId)}";
        if (from.HasValue)       query += $"&from={from.Value:O}";
        if (to.HasValue)         query += $"&to={to.Value:O}";

        var response = await _httpClient.GetAsync(query);
        if (response.IsSuccessStatusCode)
            return Result.Success(await response.Content.ReadFromJsonAsync<PagedTelemetryResult>() ?? new PagedTelemetryResult());

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<PagedTelemetryResult>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load analytics", (int)response.StatusCode));
    }

    public async Task<Result<AnalyticsSummaryDto>> GetSummaryAsync()
    {
        var response = await _httpClient.GetAsync("api/analytics/summary");
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<AnalyticsSummaryDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<AnalyticsSummaryDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<AnalyticsSummaryDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load summary", (int)response.StatusCode));
    }
}
