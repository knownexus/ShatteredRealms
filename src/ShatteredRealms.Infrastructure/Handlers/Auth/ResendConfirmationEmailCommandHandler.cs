using MediatR;
using Microsoft.Extensions.Configuration;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public ResendConfirmationEmailCommandHandler(
        IUserService userService,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _userService = userService;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<Result> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        // Always succeed — never reveal whether an email address is registered
        var userResult = await _userService.GetUserByEmailAsync(request.Email, cancellationToken);
        if (userResult.IsFailure || userResult.Value.EmailConfirmed)
        {
            return Result.Success();
        }

        var user  = userResult.Value;
        var token = await _userService.GenerateEmailConfirmationTokenAsync(user, cancellationToken);
        var webBaseUrl = _configuration["WebBaseUrl"] ?? "https://localhost:7001";
        var link = $"{webBaseUrl}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await _emailService.SendEmailConfirmationAsync(
            user.Email!, $"{user.FirstName} {user.LastName}", link, cancellationToken);

        return Result.Success();
    }
}
