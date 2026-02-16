using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ShatteredRealms.API.Extensions;

public static class ConfigurationExtension
{
    public static Logger ConfigureLogger()
    {
        var configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json"
                                      , optional: true)
                           .AddEnvironmentVariables()
                           .Build();


        var loggerConfig = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        return loggerConfig;
    }
}