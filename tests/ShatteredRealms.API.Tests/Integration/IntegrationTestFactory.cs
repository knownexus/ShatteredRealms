using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.API.Tests.Integration;

public sealed class IntegrationTestFactory : WebApplicationFactory<Program>, IDisposable
{
    // Keep the connection open for the lifetime of the factory.
    // SQLite in-memory databases are destroyed when the last connection closes.
    private readonly SqliteConnection _connection;

    public IntegrationTestFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((_, config) =>
        {
            // Inject test-only seed passwords via in-memory configuration.
            // These are test values only and never appear in production config.
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SeedPasswords:System"]         = "TestSystem@123!",
                ["SeedPasswords:Admin"]          = "TestAdmin@123!",
                ["SeedPasswords:EventOrganizer"] = "TestOrganizer@123!",
                ["SeedPasswords:User"]           = "TestUser@123!",
                ["Jwt:Key"]                      = "TestJwtSecretKeyForIntegrationTests_32chars!",
                ["Jwt:Issuer"]                   = "TestIssuer",
                ["Jwt:Audience"]                 = "TestAudience",
            });
        });

        builder.ConfigureServices(services =>
        {
            var toRemove = services
                .Where(d =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>) ||
                    d.ServiceType == typeof(DbContextOptions) ||
                    d.ServiceType == typeof(ApplicationDbContext) ||
                    (d.ServiceType.FullName != null && (
                        d.ServiceType.FullName.Contains("DbContextOptions") ||
                        d.ServiceType.FullName.Contains("IDbContextOptionsConfiguration"))))
                .ToList();

            foreach (var descriptor in toRemove)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(_connection));
        });
    }

    public new void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
        base.Dispose();
    }
}
