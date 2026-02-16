using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IPermissionService _permissionService;
    private readonly ApplicationDbContext _context;

    public RegisterCommandHandler(
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

    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var createRequest = new Application.DTOs.Users.CreateUserRequest
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var userResult = await _userService.CreateUserAsync(createRequest, cancellationToken);
        if (userResult.IsFailure)
        {
            return Result.Failure<AuthResponse>(userResult.Error);
        }

        return await AuthHelpers.GenerateAuthResponseAsync(
            userResult.Value, _userService, _tokenService, _permissionService, _context, cancellationToken);
    }
}
