using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Domain.ValueObjects;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Auth;
using ShatteredRealms.Infrastructure.Services;

namespace ShatteredRealms.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        if (environment.IsEnvironment("Testing"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = Password.RequireDigit;
            options.Password.RequireLowercase = Password.RequireLowercase;
            options.Password.RequireUppercase = Password.RequireUppercase;
            options.Password.RequireNonAlphanumeric = Password.RequireNonAlphanumeric;
            options.Password.RequiredLength = Password.RequiredLength;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
        var key = Encoding.UTF8.GetBytes(jwtKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Log.Warning("JWT authentication failed: {Message}", context.Exception.Message);
                    return Task.CompletedTask;
                }, OnTokenValidated = context =>
                {
                    Log.Debug("JWT token validated for UserId: {UserId}",
                              context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ??
                              "Unknown");

                    // --- add this block to map Permission claims ---
                    if (context.Principal != null && context.SecurityToken is System.IdentityModel.Tokens.Jwt.JwtSecurityToken token)
                    {
                        var identity = (System.Security.Claims.ClaimsIdentity)context.Principal.Identity!;

                        // Add all Permission claims from JWT into the ClaimsPrincipal
                        foreach (var c in token.Claims.Where(c => c.Type == "Permission"))
                        {
                            identity.AddClaim(new System.Security.Claims.Claim("Permission", c.Value));
                        }
                    }

                    return Task.CompletedTask;
                }
               ,
                OnChallenge = context =>
                {
                    Log.Warning("JWT challenge triggered: {Error}", context.ErrorDescription);
                    return Task.CompletedTask;
                },
            };
        });

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            // Dynamically register a policy for every permission defined in Claims
            foreach (var permission in Claims.PermissionCatalog.Select(p => p.ClaimValue))
            {
                options.AddPolicy(
                                  $"permission:{permission}",
                                  policy => policy
                                           .RequireAuthenticatedUser()
                                           .AddRequirements(new PermissionRequirement(permission)));
            }
        });
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Infrastructure services (called by MediatR handlers)
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IWikiService, WikiService>();
        services.AddScoped<IForumService, ForumService>();
        services.AddScoped<IPermissionService, PermissionService>();

        // MediatR - scan the Infrastructure assembly for all handlers
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly));

        // Needed to read the real client IP in auth handlers
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ShatteredRealms API",
                Version = "v1",
                Description = "Domain-Driven Design API with JWT Authentication and Role-Based Authorization",
                Contact = new OpenApiContact { Name = "Shattered Realms Team" },
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token in the format: Bearer {your token}",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    },
                    Array.Empty<string>()
                },
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorClient", policy =>
            {
                policy.WithOrigins("https://localhost:7001", "http://localhost:5001")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
