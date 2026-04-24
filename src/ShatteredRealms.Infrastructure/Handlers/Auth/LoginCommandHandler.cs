using MediatR;
using Microsoft.Extensions.Options;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Settings;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IPermissionService _permissionService;
    private readonly ApplicationDbContext _context;
    private readonly IOptionsMonitor<ConfirmationSettings> _confirmationSettings;

    public LoginCommandHandler(
        IUserService userService,
        ITokenService tokenService,
        IPermissionService permissionService,
        ApplicationDbContext context,
        IOptionsMonitor<ConfirmationSettings> confirmationSettings)
    {
        _userService = userService;
        _tokenService = tokenService;
        _permissionService = permissionService;
        _context = context;
        _confirmationSettings = confirmationSettings;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userService.GetUserByEmailAsync(request.Email, cancellationToken);
        if (userResult.IsFailure)
        {
            // Return generic credentials error — do not reveal whether the email exists
            return Result.Failure<AuthResponse>(DomainErrors.Authentication.InvalidCredentials);
        }

        var user = userResult.Value;

        var validationResult = User.Validate(user);
        if (validationResult.IsFailure)
        {
            return Result.Failure<AuthResponse>(validationResult.Error);
        }

        var isPasswordValid = await _userService.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return Result.Failure<AuthResponse>(DomainErrors.Authentication.InvalidCredentials);
        }

        // Read CurrentValue at request time so live appsettings changes take effect immediately
        if (_confirmationSettings.CurrentValue.RequireEmailConfirmation && !user.EmailConfirmed)
        {
            return Result.Failure<AuthResponse>(DomainErrors.Authentication.EmailNotConfirmed);
        }

        var isUnverified = await _context.UserRoles
            .AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == Claims.Roles.UnverifiedId, cancellationToken);

        if (isUnverified)
        {
            return Result.Failure<AuthResponse>(DomainErrors.Authentication.PendingApproval);
        }

        return await AuthHelpers.GenerateAuthResponseAsync(
            user, _userService, _tokenService, _permissionService, _context, cancellationToken);
    }
}
