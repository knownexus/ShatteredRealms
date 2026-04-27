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
        string? actorSearch = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var query = $"api/analytics?page={page}&pageSize={pageSize}";
        if (eventType.HasValue)     query += $"&eventType={(int)eventType.Value}";
        if (!string.IsNullOrEmpty(actorSearch)) query += $"&actorSearch={Uri.EscapeDataString(actorSearch)}";
        if (from.HasValue)          query += $"&from={from.Value:O}";
        if (to.HasValue)            query += $"&to={to.Value:O}";

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

    public async Task<Result> FlagEventAsync(Guid eventId, string? reason)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/analytics/{eventId}/flag", new { reason });
        if (response.IsSuccessStatusCode) return Result.Success();
        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to flag event", (int)response.StatusCode));
    }

    public async Task<Result> UnflagEventAsync(Guid eventId)
    {
        var response = await _httpClient.DeleteAsync($"api/analytics/{eventId}/flag");
        if (response.IsSuccessStatusCode) return Result.Success();
        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to unflag event", (int)response.StatusCode));
    }

    public async Task<Result<List<AnalyticsFlagRuleDto>>> GetRulesAsync()
    {
        var response = await _httpClient.GetAsync("api/analytics/rules");
        if (response.IsSuccessStatusCode)
            return Result.Success(await response.Content.ReadFromJsonAsync<List<AnalyticsFlagRuleDto>>() ?? []);
        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<AnalyticsFlagRuleDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load rules", (int)response.StatusCode));
    }

    public async Task<Result<AnalyticsFlagRuleDto>> CreateRuleAsync(CreateFlagRulePayload payload)
    {
        var response = await _httpClient.PostAsJsonAsync("api/analytics/rules", payload);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<AnalyticsFlagRuleDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<AnalyticsFlagRuleDto>(new Error("Error", "Empty response", 500));
        }
        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<AnalyticsFlagRuleDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to create rule", (int)response.StatusCode));
    }

    public async Task<Result> DeleteRuleAsync(int ruleId)
    {
        var response = await _httpClient.DeleteAsync($"api/analytics/rules/{ruleId}");
        if (response.IsSuccessStatusCode) return Result.Success();
        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to delete rule", (int)response.StatusCode));
    }

    public async Task<Result> SetRuleActiveAsync(int ruleId, bool isActive)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/analytics/rules/{ruleId}/active", new { isActive });
        if (response.IsSuccessStatusCode) return Result.Success();
        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to update rule", (int)response.StatusCode));
    }
}

public record CreateFlagRulePayload(
    FlagRuleType RuleType,
    string? TargetUserId,
    TelemetryEventType? EventType,
    string? ActorRole,
    string Reason
);
