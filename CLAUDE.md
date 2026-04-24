# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build entire solution
dotnet build ShatteredRealms.sln

# Run API (https://localhost:7000, http://localhost:5000, Swagger at /swagger)
cd src/ShatteredRealms.API && dotnet run

# Run Web frontend (https://localhost:7001, http://localhost:5001)
cd src/ShatteredRealms.Web && dotnet run

# Run all tests
dotnet test ShatteredRealms.sln

# Run tests for a single project
dotnet test tests/ShatteredRealms.Domain.Tests

# Run a specific test
dotnet test tests/ShatteredRealms.API.Tests --filter "FullyQualifiedName~TestName"

# Add EF Core migration
cd src/ShatteredRealms.Infrastructure
dotnet ef migrations add MigrationName --startup-project ../ShatteredRealms.API

# Apply migrations
dotnet ef database update --startup-project ../ShatteredRealms.API
```

## Architecture

Clean Architecture with DDD across 5 projects plus 4 test projects:

```
Domain → Application → Infrastructure → API
                                      → Web
```

- **Domain**: Entities, value objects (`Email`, `FirstName`, etc.), `Result<T>` error pattern, shared abstractions. No external dependencies.
- **Application**: MediatR commands/queries (CQRS), DTOs, service interfaces, AutoMapper profiles. Defines the contract; does not implement.
- **Infrastructure**: EF Core `ApplicationDbContext`, MediatR handlers (the implementations), services (`UserService`, `TokenService`, etc.), migrations. SQL Server with ASP.NET Core Identity.
- **API**: ASP.NET Core controllers, JWT auth, permission-based authorization filters, DI wiring in `Extensions/`.
- **Web**: Blazor Server frontend, calls the API via typed `HttpClient` services.

### CQRS Flow

`Controller` → `IMediator.Send(Command/Query)` → `Handler` (in Infrastructure) → `Service` → EF Core → SQL Server

Commands and queries live in `Application/Features/{Domain}/`, handlers in `Infrastructure/Handlers/{Domain}/`.

### Error Handling

Use `Result<T>` (in `Domain/Shared/`) throughout — handlers return `Result<T>`, not raw values or thrown exceptions. API controllers unwrap results and map to appropriate HTTP responses.

### Authorization

Permission-based RBAC. 60+ named permissions (e.g., `Users.View`, `Forum.Thread.Create`) are stored as ASP.NET Identity role claims. The `[RequirePermission("...")]` filter (in `API/Authorization/`) validates them. Five built-in roles: System (100), Admin (90), Analyst (80), EventOrganizer (50), User (10) — priority determines conflict resolution.

### Soft Deletes

Forum and Wiki entities use EF Core global query filters for soft deletes. Always check whether new queries need `IgnoreQueryFilters()`.

### Version Control

- Stage and locally commit groups of logical changes before moving onto a new change.
- Format commits as follows : 
[Brief Summary of changes]
[Empty Line]
[Bullet Point list of changes made]
Files: 
	[List of files changed]
- Use the author `Phillip Smyth <knownexus@gmail.com>`

### Database

SQL Server. Connection string in `appsettings.json` is dev-only (`sa / P@$$w0rd`). DB is seeded on first run with default users:
- `admin@shatteredrealms.com` / `Admin@123!`
- `user@shatteredrealms.com` / `User@123`

### Testing

- Unit tests use NSubstitute for mocks and FluentAssertions for assertions.
- Integration tests (`API.Tests`) use `WebApplicationFactory` and SQLite (in-memory) — see `appsettings.Testing.json`.
- `Infrastructure.Tests` may hit a real DB; check test fixture setup before running.
- When making changes write or modify a Unit test first against the expected functionality, before writing the functional change.

