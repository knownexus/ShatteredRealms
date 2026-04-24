using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Auth.Commands;

namespace ShatteredRealms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new RegisterCommand(request.Email, request.Password, request.FirstName, request.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogDebug("Register failed - Code: {Code} {Title}", result.Error.Code, result.Error.Title);
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        _logger.LogInformation("Registration successful for {Email}", request.Email);
        return Ok(result.Value);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromBody] ConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ConfirmEmailCommand(request.UserId, request.Token),
            cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogDebug("Email confirmation failed - Code: {Code} {Title}", result.Error.Code, result.Error.Title);
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(new { message = "Email confirmed successfully. You may now sign in." });
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmation(
        [FromBody] ResendConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        // Always return 200 — never reveal whether an email address is registered
        await _mediator.Send(new ResendConfirmationEmailCommand(request.Email), cancellationToken);
        return Ok(new { message = "If that address is registered and unconfirmed, a new link has been sent." });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginCommand(request.Email, request.Password), cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogDebug("Login failed - Code: {Code} {Title}", result.Error.Code, result.Error.Title);
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        _logger.LogInformation("Login successful - UserId: {UserId}", result.Value.User.Id);
        return Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponse>> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(request.RefreshToken), cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogDebug("Token refresh failed - Code: {Code} {Title}", result.Error.Code, result.Error.Title);
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var result = await _mediator.Send(new RevokeTokenCommand(request.RefreshToken, ipAddress), cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogDebug("Token revoke failed - Code: {Code} {Title}", result.Error.Code, result.Error.Title);
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return NoContent();
    }
}
