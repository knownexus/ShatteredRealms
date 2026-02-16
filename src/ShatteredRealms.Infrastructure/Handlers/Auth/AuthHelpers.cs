using Microsoft.AspNetCore.Http;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

/// <summary>
/// Shared logic for generating an AuthResponse after a successful authentication event.
/// Extracted here to avoid duplication across Register, Login, and RefreshToken handlers.
/// </summary>
internal static class AuthHelpers
{
    internal static async Task<Result<AuthResponse>> GenerateAuthResponseAsync(
        User user,
        IUserService userService,
        ITokenService tokenService,
        IPermissionService permissionService,
        ApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var validationResult = User.Validate(user);
        if (validationResult.IsFailure)
        {
            return Result.Failure<AuthResponse>(validationResult.Error);
        }

        var rolesResult = await userService.GetUserRolesAsync(user.Id, cancellationToken);
        if (rolesResult.IsFailure)
        {
            return Result.Failure<AuthResponse>(rolesResult.Error);
        }

        var permissionsResult = await permissionService.GetUserPermissionsAsync(user.Id, cancellationToken);
        if (permissionsResult.IsFailure)
        {
            return Result.Failure<AuthResponse>(permissionsResult.Error);
        }

        var accessToken = tokenService.GenerateAccessToken(
            user.Id, user.Email!, rolesResult.Value, permissionsResult.Value);

        var refreshToken = tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = string.Empty, // Set by caller via IHttpContextAccessor
            UserId = user.Id,
        };

        context.RefreshToken.Add(refreshTokenEntity);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = rolesResult.Value,
                Permissions = permissionsResult.Value,
            },
        });
    }
}
