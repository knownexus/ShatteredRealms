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
    public async Task<ActionResult<AuthResponse>> Register(
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

        _logger.LogInformation("Registration successful - UserId: {UserId}", result.Value.User.Id);
        return Ok(result.Value);
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
