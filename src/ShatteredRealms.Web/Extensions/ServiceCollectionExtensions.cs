using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShatteredRealms.Web.Services;

namespace ShatteredRealms.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Blazored LocalStorage
        services.AddBlazoredLocalStorage();

        // Register a local memory cache
        services.AddSingleton<AuthMemoryCache>();

        // Register auth state service
        services.AddScoped<AuthStateService>();

        // Register the auth token handler
        services.AddScoped<AuthTokenHandler>();

        // Get API base address from configuration or use default
        var apiBaseAddress = configuration["ApiSettings:BaseAddress"] ?? "https://localhost:7000";

        // Configure HttpClients - AuthService doesn't need the token handler
        services.AddHttpClient("AuthClient", client =>
        {
            client.BaseAddress = new Uri(apiBaseAddress);
        })
        .AddDevelopmentCertificateHandler();

        services.AddScoped<AuthService>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient("AuthClient");
            return new AuthService(
                                   httpClient,
                                   sp.GetRequiredService<ILocalStorageService>(),
                                   sp.GetRequiredService<AuthMemoryCache>(),
                                   sp.GetRequiredService<AuthStateService>()
                                  );
        });

        services.AddHttpClient<WikiClientService>(client =>
         {
             client.BaseAddress = new Uri(apiBaseAddress);
         })
        .AddHttpMessageHandler<AuthTokenHandler>()
        .AddDevelopmentCertificateHandler();

        services.AddHttpClient<ForumClientService>(client =>
         {
             client.BaseAddress = new Uri(apiBaseAddress);
         })
        .AddHttpMessageHandler<AuthTokenHandler>()
        .AddDevelopmentCertificateHandler();

        services.AddHttpClient<UserService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseAddress);
        })
        .AddHttpMessageHandler<AuthTokenHandler>()
        .AddDevelopmentCertificateHandler();

        services.AddHttpClient<RoleService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseAddress);
        })
        .AddHttpMessageHandler<AuthTokenHandler>()
        .AddDevelopmentCertificateHandler();

        services.AddHttpClient<PermissionService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseAddress);
        })
        .AddHttpMessageHandler<AuthTokenHandler>()
        .AddDevelopmentCertificateHandler();

        return services;
    }

    private static IHttpClientBuilder AddDevelopmentCertificateHandler(this IHttpClientBuilder builder)
    {
        return builder.ConfigurePrimaryHttpMessageHandler(sp =>
        {
            var env = sp.GetRequiredService<IHostEnvironment>();
            var handler = new HttpClientHandler();

            if (env.IsDevelopment())
            {
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            }

            return handler;
        });
    }
}
