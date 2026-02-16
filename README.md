# ShatteredRealms - Domain-Driven Design Solution

A complete Domain-Driven Design (DDD) solution with Clean Architecture, featuring a Blazor frontend and ASP.NET Core Web API backend with JWT authentication, role-based permissions, and Microsoft Identity.

## Architecture

This solution follows Clean Architecture principles with Domain-Driven Design:

- **ShatteredRealms.Domain**: Core domain entities and business logic
- **ShatteredRealms.Application**: Application DTOs, interfaces, and service contracts
- **ShatteredRealms.Infrastructure**: Data access, Identity configuration, and service implementations
- **ShatteredRealms.API**: RESTful API with JWT authentication
- **ShatteredRealms.Web**: Blazor Server frontend

## Features

- ✅ JWT Authentication with Refresh Tokens
- ✅ Role-Based Access Control (RBAC)
- ✅ Permission-Based Authorization
- ✅ User Management (Create, Update, Delete)
- ✅ Role Management with Permissions
- ✅ Microsoft Identity Integration
- ✅ Entity Framework Core with SQL Server
- ✅ Clean Architecture with DDD
- ✅ Blazor Server Frontend

## Prerequisites

- .NET 9.0 SDK
- SQL Server (localhost with sa account)
- Visual Studio 2022, Rider, or VS Code

## Database Configuration

The application uses the following connection string (configured in `src/ShatteredRealms.API/appsettings.json`):

```
Server=localhost;Database=ShatteredRealms;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=sa;Password=P@$$w0rd;TrustServerCertificate=True;
```

## Getting Started

### 1. Build the Solution

```bash
dotnet build ShatteredRealms.sln
```

### 2. Run the API (Terminal 1)

```bash
cd src/ShatteredRealms.API
dotnet run
```

The API will start at:
- HTTPS: https://localhost:7000
- HTTP: http://localhost:5000
- **Swagger UI**: https://localhost:7000/swagger

### 3. Run the Blazor Web App (Terminal 2)

```bash
cd src/ShatteredRealms.Web
dotnet run
```

The Web app will start at:
- HTTPS: https://localhost:7001
- HTTP: http://localhost:5001

### 4. Access the Application

1. Open your browser and navigate to https://localhost:7001
2. Click "Login" in the navigation menu
3. Use the default admin credentials:
   - Email: `admin@shatteredrealms.com`
   - Password: `Admin@123`

### 5. Using Swagger API Documentation

1. Navigate to https://localhost:7000/swagger
2. To test authenticated endpoints:
   - First, call `POST /api/auth/login` with admin credentials
   - Copy the `accessToken` from the response
   - Click the "Authorize" button at the top right
   - Enter: `Bearer {your-access-token}` (replace with actual token)
   - Click "Authorize" then "Close"
   - Now you can test all authenticated endpoints

## Default Seeded Data

The application automatically seeds the following data on first run:

### Users

**Admin User**
- Email: `admin@shatteredrealms.com`
- Password: `Admin@123`
- Role: Admin (with all permissions)
- Can manage users, roles, and permissions

**Manager User**
- Email: `manager@shatteredrealms.com`
- Password: `Manager@123`
- Role: Manager
- Can manage users but cannot manage roles or permissions

**Regular User**
- Email: `user@shatteredrealms.com`
- Password: `User@123`
- Role: User
- Can only view and edit their own details

### Permissions
1. Users.View
2. Users.Create
3. Users.Update
4. Users.Delete
5. Roles.View
6. Roles.Create
7. Roles.Update
8. Roles.Delete
9. Permissions.View

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/refresh-token` - Refresh access token
- `POST /api/auth/revoke-token` - Revoke refresh token

### Users (Requires Authentication)
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create new user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Roles (Requires Authentication)
- `GET /api/roles` - Get all roles
- `GET /api/roles/{id}` - Get role by ID
- `POST /api/roles` - Create new role
- `PUT /api/roles/{id}` - Update role
- `DELETE /api/roles/{id}` - Delete role

### Permissions (Requires Authentication)
- `GET /api/permissions` - Get all permissions

## Project Structure

```
ShatteredRealms/
├── src/
│   ├── ShatteredRealms.Domain/
│   │   └── Entities/
│   │       ├── ApplicationUser.cs
│   │       ├── Role.cs
│   │       ├── Permission.cs
│   │       ├── UserRole.cs
│   │       ├── RolePermission.cs
│   │       └── RefreshToken.cs
│   ├── ShatteredRealms.Application/
│   │   ├── DTOs/
│   │   │   ├── Auth/
│   │   │   ├── Users/
│   │   │   ├── Roles/
│   │   │   └── Permissions/
│   │   └── Interfaces/
│   ├── ShatteredRealms.Infrastructure/
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   └── Services/
│   │       ├── AuthService.cs
│   │       ├── UserService.cs
│   │       ├── RoleService.cs
│   │       ├── PermissionService.cs
│   │       └── TokenService.cs
│   ├── ShatteredRealms.API/
│   │   ├── Controllers/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   └── ShatteredRealms.Web/
│       ├── Components/
│       │   ├── Pages/
│       │   │   ├── Login.razor
│       │   │   ├── Users.razor
│       │   │   └── Roles.razor
│       │   └── Layout/
│       ├── Services/
│       └── Program.cs
└── ShatteredRealms.sln
```

## Technology Stack

- **Backend**: ASP.NET Core 9.0 Web API
- **Frontend**: Blazor Server
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Identity**: Microsoft ASP.NET Core Identity
- **Architecture**: Clean Architecture with DDD

## Security Notes

⚠️ **Important**: The JWT key and connection string in `appsettings.json` are for development only.
In production:
1. Store secrets in Azure Key Vault or similar
2. Use environment variables
3. Never commit secrets to source control
4. Use strong, randomly generated JWT keys
5. Configure proper password policies

## Development

### Adding New Permissions

1. Add the permission to the `SeedPermissions` method in `ApplicationDbContext.cs`
2. Run a new migration: `dotnet ef migrations add AddNewPermission --startup-project ../ShatteredRealms.API`
3. The permission will be available for assignment to roles

### Creating Custom Roles

Use the Roles page in the web UI or the API to create custom roles with specific permissions.

## Troubleshooting

### Database Connection Issues
- Ensure SQL Server is running
- Verify the connection string in `appsettings.json`
- Check that the sa account has the correct password

### Port Conflicts
- API uses ports 7000 (HTTPS) and 5000 (HTTP)
- Web uses ports 7001 (HTTPS) and 5001 (HTTP)
- Modify `launchSettings.json` if these ports are in use

### CORS Issues
- The API is configured to accept requests from the Blazor app
- Update CORS policy in `Program.cs` if using different ports

## License

This is a sample project for educational purposes.
