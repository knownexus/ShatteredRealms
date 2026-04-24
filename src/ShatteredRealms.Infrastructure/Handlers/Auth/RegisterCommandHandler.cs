using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Settings;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IOptionsMonitor<ConfirmationSettings> _confirmationSettings;

    public RegisterCommandHandler(
        IUserService userService,
        IEmailService emailService,
        IConfiguration configuration,
        IOptionsMonitor<ConfirmationSettings> confirmationSettings)
    {
        _userService = userService;
        _emailService = emailService;
        _configuration = configuration;
        _confirmationSettings = confirmationSettings;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateUserRequest
        {
            Email     = request.Email,
            Password  = request.Password,
            FirstName = request.FirstName,
            LastName  = request.LastName,
        };

        var userResult = await _userService.CreateUserAsync(createRequest, cancellationToken);
        if (userResult.IsFailure)
        {
            return Result.Failure<RegisterResponse>(userResult.Error);
        }

        var user = userResult.Value;

        // Read CurrentValue at request time so live appsettings changes take effect immediately
        if (!_confirmationSettings.CurrentValue.RequireEmailConfirmation)
        {
            // Auto-confirm so the user can sign in immediately; keeps EmailConfirmed consistent
            var autoToken = await _userService.GenerateEmailConfirmationTokenAsync(user, cancellationToken);
            await _userService.ConfirmEmailAsync(user.Id, autoToken, cancellationToken);

            return Result.Success(new RegisterResponse(
                "Account created successfully. You can now sign in.",
                RequiresEmailConfirmation: false));
        }

        var token = await _userService.GenerateEmailConfirmationTokenAsync(user, cancellationToken);
        var webBaseUrl = _configuration["WebBaseUrl"] ?? "https://localhost:7001";
        var link = $"{webBaseUrl}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await _emailService.SendEmailConfirmationAsync(
            user.Email!, $"{user.FirstName} {user.LastName}", link, cancellationToken);

        return Result.Success(new RegisterResponse(
            "Account created. Please check your email for a confirmation link.",
            RequiresEmailConfirmation: true));
    }
}
