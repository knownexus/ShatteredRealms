using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.API.Extensions;

public static class DatabaseSeeder
{
    public static async Task SeedDatabase(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
        {
            await context.Database.MigrateAsync();
        }
        else
        {
            await context.Database.EnsureCreatedAsync();
        }

        await SeedDataAsync(services);
    }

    public static async Task SeedDataAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var configuration = services.GetRequiredService<IConfiguration>();

        await SeedPermissionsAsync(context);
        await SeedUsersAsync(context, userManager, configuration);
    }

    private static async Task SeedPermissionsAsync(ApplicationDbContext context)
    {
        var catalog = Claims.PermissionCatalog.ToDictionary(x => x.ClaimValue);

        var mapping = new List<(string RoleId, IReadOnlyList<string> ClaimValues)>
        {
            (Claims.Roles.SystemId, Claims.RolePermissions.System),
            (Claims.Roles.AdminId, Claims.RolePermissions.Admin),
            (Claims.Roles.AnalystId, Claims.RolePermissions.Analyst),
            (Claims.Roles.EventOrganizerId, Claims.RolePermissions.EventOrganizer),
            (Claims.Roles.UserId, Claims.RolePermissions.User),
        };

        foreach (var (roleId, claimValues) in mapping)
        {
            foreach (var claimValue in claimValues)
            {
                if (!catalog.TryGetValue(claimValue, out var def))
                {
                    continue;
                }

                var exists = await context.Set<Permission>()
                    .AnyAsync(p => p.RoleId == roleId && p.ClaimValue == claimValue);

                if (exists)
                {
                    continue;
                }

                context.Set<Permission>().Add(new Permission
                {
                    RoleId = roleId,
                    ClaimType = "permission",
                    ClaimValue = claimValue,
                    Description = def.Description,
                    Category = def.Category
                });
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedUsersAsync(
        ApplicationDbContext context,
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        // Seed passwords are read from configuration (appsettings / env vars / secrets).
        // They are never hardcoded and never exposed to the client.
        await CreateUserIfNotExists(context, userManager,
            email: "system@shatteredrealms.com",
            firstName: "System", lastName: "Account",
            password: configuration["SeedPasswords:System"] ?? throw new InvalidOperationException("SeedPasswords:System not configured"),
            roleId: Claims.Roles.SystemId);

        await CreateUserIfNotExists(context, userManager,
            email: "admin@shatteredrealms.com",
            firstName: "Admin", lastName: "User",
            password: configuration["SeedPasswords:Admin"] ?? throw new InvalidOperationException("SeedPasswords:Admin not configured"),
            roleId: Claims.Roles.AdminId);

        await CreateUserIfNotExists(context, userManager,
                                    email: "analyst@shatteredrealms.com",
                                    firstName: "Analyst", lastName: "User",
                                    password: configuration["SeedPasswords:Analyst"] ??
                                              throw new InvalidOperationException("SeedPasswords:Analyst not configured"),
                                    roleId: Claims.Roles.AnalystId);

        await CreateUserIfNotExists(context, userManager,
            email: "organizer@shatteredrealms.com",
            firstName: "Event", lastName: "Organizer",
            password: configuration["SeedPasswords:EventOrganizer"] ?? throw new InvalidOperationException("SeedPasswords:EventOrganizer not configured"),
            roleId: Claims.Roles.EventOrganizerId);

        await CreateUserIfNotExists(context, userManager,
            email: "testuser@shatteredrealms.com",
            firstName: "Test", lastName: "User",
            password: configuration["SeedPasswords:User"] ?? throw new InvalidOperationException("SeedPasswords:User not configured"),
            roleId: Claims.Roles.UserId);
    }

    private static async Task CreateUserIfNotExists(
        ApplicationDbContext context,
        UserManager<User> userManager,
        string email, string firstName, string lastName,
        string password, string roleId)
    {
        if (await userManager.FindByEmailAsync(email) != null)
        {
            return;
        }

        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Failed to seed user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        context.Set<UserRole>().Add(new UserRole { UserId = user.Id, RoleId = roleId });
        await context.SaveChangesAsync();
    }
}
