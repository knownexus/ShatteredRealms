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

- .NET 10.0 SDK
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
- Password: `Admin@123!`
- Role: Admin (all permissions)

**EventOrganizer User**
- Email: `organizer@shatteredrealms.com`
- Password: `Organizer@123!`
- Role: EventOrganizer
- Can manage characters, wiki, forum moderation, videos, and announcements

**Analyst User**

- Email: `analyst@shatteredrealms.com`
- Password: `Analyst@123!`
- Role: Analyst
- Can manage characters, wiki, forum moderation, videos, and announcements

**Regular User**
- Email: `testuser@shatteredrealms.com`
- Password: `TestUser@123!`
- Role: User
- Can view and edit their own profile, characters, and contribute to forum/wiki

### Roles

| Role | Description |
|------|-------------|
| System | Internal system role, unrestricted |
| Admin | Full administrative access |
| EventOrganizer | Can manage events, characters, assign positions/experience |
| Analyst | Report access only |
| User | Standard registered user |

### Permissions

The system defines 60 permissions across the following categories:

**Users** — `Users.ViewOwn`, `Users.UpdateOwn`, `Users.View`, `Users.Create`, `Users.Update`, `Users.Delete`

**Characters** — `Characters.ViewOwn`, `Characters.CreateOwn`, `Characters.Create`, `Characters.UpdateOwn`, `Characters.View`, `Characters.Update`, `Characters.Delete`, `Characters.DeleteOwn`, `Characters.AssignPosition`, `Characters.AssignExperience`

**Roles** — `Role.View`, `Role.Create`, `Role.Update`, `Role.Delete`, `Role.Assign`

**PermissionControl** — `PermissionControl.View`, `PermissionControl.Assign`

**Forum (Category)** — `Forum.Category.Create`, `Forum.Category.Update`, `Forum.Category.Delete`

**Forum (Thread)** — `Forum.Thread.Create`, `Forum.Thread.UpdateOwn`, `Forum.Thread.Update`, `Forum.Thread.DeleteOwn`, `Forum.Thread.Delete`, `Forum.Thread.Lock`, `Forum.Thread.Pin`

**Forum (Post)** — `Forum.Post.Create`, `Forum.Post.UpdateOwn`, `Forum.Post.Update`, `Forum.Post.DeleteOwn`, `Forum.Post.Delete`

**Wiki** — `Wiki.Page.Create`, `Wiki.Page.UpdateOwn`, `Wiki.Page.Update`, `Wiki.Page.DeleteOwn`, `Wiki.Page.Delete`, `Wiki.Category.Manage`

**Videos** — `Videos.View`, `Videos.Create`, `Videos.Update`, `Videos.Delete`, `Videos.DeleteOwn`, `Videos.Approve`

**ActivityLog** — `ActivityLog.View`, `ActivityLog.Update`, `ActivityLog.Delete`

**Reports** — `Reports.View`, `Reports.Create`, `Reports.CreateAll`

**Announcements** — `Announcements.View`, `Announcements.Create`, `Announcements.Update`, `Announcements.Delete`

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/refresh-token` - Refresh access token
- `POST /api/auth/revoke-token` - Revoke refresh token (requires authentication)

### Users (Requires Authentication)
- `GET /api/users` - Get all users (`Users.View`)
- `GET /api/users/{id}` - Get user by ID (`Users.View`)
- `GET /api/users/self` - Get own profile (`Users.ViewOwn`)
- `POST /api/users` - Create new user (`Users.Create`)
- `PUT /api/users/self` - Update own profile (`Users.UpdateOwn`)
- `PUT /api/users/{id}` - Update user (`Users.Update`)
- `DELETE /api/users/{id}` - Delete user (`Users.Delete`)

### Roles (Requires Authentication)
- `GET /api/roles` - Get all roles (`Role.View`)
- `GET /api/roles/{id}` - Get role by ID (`Role.View`)
- `POST /api/roles` - Create new role (`Role.Create`)
- `PUT /api/roles/{id}` - Update role (`Role.Update`)
- `DELETE /api/roles/{id}` - Delete role (`Role.Delete`)

### Permissions (Requires Authentication)
- `GET /api/permissions` - Get all permissions (`PermissionControl.View`)

### Forum (Requires Authentication)
- `GET /api/forum/categories` - List all forum categories
- `POST /api/forum/categories` - Create a category (`Forum.Category.Create`)
- `PUT /api/forum/categories/{id}` - Update a category (`Forum.Category.Update`)
- `DELETE /api/forum/categories/{id}` - Delete a category (`Forum.Category.Delete`)
- `GET /api/forum/categories/{id}/threads` - List threads in a category
- `POST /api/forum/threads` - Create a thread (`Forum.Thread.Create`)
- `GET /api/forum/threads/{id}` - Get a thread and its posts
- `PUT /api/forum/threads/{id}` - Update any thread (`Forum.Thread.Update`)
- `PUT /api/forum/threads/{id}/self` - Update own thread (`Forum.Thread.UpdateOwn`)
- `DELETE /api/forum/threads/{id}` - Delete any thread (`Forum.Thread.Delete`)
- `DELETE /api/forum/threads/{id}/self` - Delete own thread (`Forum.Thread.DeleteOwn`)
- `PUT /api/forum/threads/{id}/pin` - Pin/unpin a thread (`Forum.Thread.Pin`)
- `PUT /api/forum/threads/{id}/lock` - Lock/unlock a thread (`Forum.Thread.Lock`)
- `POST /api/forum/threads/{id}/posts` - Post a reply (`Forum.Post.Create`)
- `PUT /api/forum/posts/{id}` - Update any post (`Forum.Post.Update`)
- `PUT /api/forum/posts/{id}/self` - Update own post (`Forum.Post.UpdateOwn`)
- `DELETE /api/forum/posts/{id}` - Delete any post (`Forum.Post.Delete`)
- `DELETE /api/forum/posts/{id}/self` - Delete own post (`Forum.Post.DeleteOwn`)

### Wiki (Requires Authentication)
- `GET /api/wiki` - List all wiki pages
- `GET /api/wiki/{id}` - Get a wiki page
- `POST /api/wiki` - Create a wiki page (`Wiki.Page.Create`)
- `PUT /api/wiki/{id}` - Update any wiki page (`Wiki.Page.Update`)
- `PUT /api/wiki/own/{id}` - Update own wiki page (`Wiki.Page.UpdateOwn`)
- `DELETE /api/wiki/{id}` - Delete any wiki page (`Wiki.Page.Delete`)
- `DELETE /api/wiki/own/{id}` - Delete own wiki page (`Wiki.Page.DeleteOwn`)
- `POST /api/wiki/category` - Create/update/delete wiki category (`Wiki.Category.Manage`)

## Project Structure

```
ShatteredRealms/
├── src/
│   ├── ShatteredRealms.Domain/
│   │   ├── Entities/
│   │   │   ├── User/
│   │   │   │   ├── User.cs
│   │   │   │   ├── UserRole.cs
│   │   │   │   ├── EmergencyContact.cs
│   │   │   │   └── UserEmergencyContact.cs
│   │   │   ├── Claims/
│   │   │   │   ├── Role.cs
│   │   │   │   └── Permission.cs
│   │   │   ├── Forum/
│   │   │   │   ├── ForumCategory.cs
│   │   │   │   ├── ForumThread.cs
│   │   │   │   └── ForumPost.cs
│   │   │   ├── Wiki/
│   │   │   │   ├── WikiPage.cs
│   │   │   │   ├── WikiCategory.cs
│   │   │   │   ├── WikiPageCategory.cs
│   │   │   │   └── WikiRevision.cs
│   │   │   ├── ActivityLog/
│   │   │   │   └── ActivityLog.cs
│   │   │   └── RefreshToken.cs
│   │   ├── Shared/
│   │   │   ├── Claims.cs
│   │   │   ├── Result.cs
│   │   │   └── Error.cs
│   │   └── ValueObjects/
│   │       ├── Email.cs
│   │       ├── FirstName.cs
│   │       ├── LastName.cs
│   │       ├── Password.cs
│   │       └── PhoneNumber.cs
│   ├── ShatteredRealms.Application/
│   │   ├── DTOs/
│   │   │   ├── Auth/
│   │   │   ├── Users/
│   │   │   ├── Roles/
│   │   │   ├── Forum/
│   │   │   ├── Wiki/
│   │   │   └── Permissions/
│   │   ├── Features/         (Commands & Queries per domain)
│   │   │   ├── Auth/
│   │   │   ├── Users/
│   │   │   ├── Roles/
│   │   │   ├── Forum/
│   │   │   ├── Wiki/
│   │   │   └── Permissions/
│   │   └── Interfaces/
│   ├── ShatteredRealms.Infrastructure/
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   ├── Handlers/         (MediatR command/query handlers)
│   │   │   ├── Auth/
│   │   │   ├── Users/
│   │   │   ├── Roles/
│   │   │   ├── Forum/
│   │   │   └── Wiki/
│   │   ├── Services/
│   │   │   ├── UserService.cs
│   │   │   ├── RoleService.cs
│   │   │   ├── PermissionService.cs
│   │   │   ├── TokenService.cs
│   │   │   ├── ForumService.cs
│   │   │   └── WikiService.cs
│   │   └── Migrations/
│   ├── ShatteredRealms.API/
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── UsersController.cs
│   │   │   ├── RolesController.cs
│   │   │   ├── PermissionsController.cs
│   │   │   ├── ForumController.cs
│   │   │   └── WikiController.cs
│   │   ├── Authorization/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   └── ShatteredRealms.Web/
│       ├── Components/
│       │   ├── Pages/
│       │   │   ├── Login.razor
│       │   │   ├── Users.razor
│       │   │   ├── Roles.razor
│       │   │   ├── Permissions.razor
│       │   │   ├── Forum/
│       │   │   └── Wiki/
│       │   └── Layout/
│       ├── Services/
│       └── Program.cs
├── tests/
│   ├── ShatteredRealms.Domain.Tests/
│   ├── ShatteredRealms.Application.Tests/
│   ├── ShatteredRealms.Infrastructure.Tests/
│   └── ShatteredRealms.API.Tests/
└── ShatteredRealms.sln
```

## Technology Stack

- **Backend**: ASP.NET Core 10.0 Web API
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

1. Append the new `PermissionDef` entry to `PermissionCatalog` in `src/ShatteredRealms.Domain/Shared/Claims.cs`
2. Add it to the appropriate `RolePermissions` arrays in the same file
3. Run a new migration: `dotnet ef migrations add AddNewPermission --startup-project ../ShatteredRealms.API`
4. The permission will be available for assignment to roles

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
