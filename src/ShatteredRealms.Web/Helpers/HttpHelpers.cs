using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Helpers;

public static class HttpHelpers
{
    public static async Task<Result<T>> GetResult<T>(Func<Task<HttpResponseMessage>> call)
    {
        HttpResponseMessage response;

        try
        {
            response = await call();
        }
        catch (Exception ex)
        {
            return Result.Failure<T>(new Error("Request failed", ex.Message, 0));
        }

        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<T>();
            return dto is not null
                ? Result.Success(dto)
                : Result.Failure<T>(new Error("Empty response", "Server returned no data", 500));
        }

        try
        {
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            return Result.Failure<T>(new Error(
                                               problem?.Title ?? response.ReasonPhrase ?? "Error",
                                               problem?.Detail ?? "No detail provided",
                                               (int)response.StatusCode));
        }
        catch
        {
            return Result.Failure<T>(new Error(
                                               response.ReasonPhrase ?? "Error",
                                               $"HTTP {(int)response.StatusCode}",
                                               (int)response.StatusCode));
        }
    }
}