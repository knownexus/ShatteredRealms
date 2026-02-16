using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IPermissionService _permissionService;

    public RefreshTokenCommandHandler(
        ApplicationDbContext context,
        IUserService userService,
        ITokenService tokenService,
        IPermissionService permissionService)
    {
        _context = context;
        _userService = userService;
        _tokenService = tokenService;
        _permissionService = permissionService;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _context.RefreshToken
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (storedToken == null || !storedToken.IsActive)
        {
            return Result.Failure<AuthResponse>(DomainErrors.Authentication.InvalidRefreshToken);
        }

        // Revoke old token
        storedToken.RevokedAt = DateTime.UtcNow;

        var authResult = await AuthHelpers.GenerateAuthResponseAsync(
            storedToken.User, _userService, _tokenService, _permissionService, _context, cancellationToken);

        if (authResult.IsFailure)
        {
            return authResult;
        }

        // Link the new token back to the old one for audit purposes
        var newToken = await _context.RefreshToken
            .FirstOrDefaultAsync(rt => rt.Token == authResult.Value.RefreshToken, cancellationToken);

        if (newToken != null)
        {
            storedToken.ReplacedByToken = newToken.Token;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return authResult;
    }
}
