using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IPermissionService _permissionService;
    private readonly ApplicationDbContext _context;

    public LoginCommandHandler(
        IUserService userService,
        ITokenService tokenService,
        IPermissionService permissionService,
        ApplicationDbContext context)
    {
        _userService = userService;
        _tokenService = tokenService;
        _permissionService = permissionService;
        _context = context;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userService.GetUserByEmailAsync(request.Email, cancellationToken);
        if (userResult.IsFailure)
        {
            // Return generic credentials error - do not reveal whether email exists
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

        return await AuthHelpers.GenerateAuthResponseAsync(
            user, _userService, _tokenService, _permissionService, _context, cancellationToken);
    }
}
