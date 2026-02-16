using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ShatteredRealms.API.Controllers;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Tests.Controllers;

public sealed class AuthControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<AuthController> _logger = Substitute.For<ILogger<AuthController>>();
    private readonly AuthController _controller;

    private static AuthResponse BuildAuthResponse(string userId = "user-1") => new()
    {
        AccessToken = "access-token",
        RefreshToken = "refresh-token",
        User = new UserDto
        {
            Id = userId,
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Roles = new List<string> { "User" },
        },
    };

    public AuthControllerTests()
    {
        _controller = new AuthController(_mediator, _logger);

        // Provide a minimal HttpContext so HttpContext.Connection is not null
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext(),
        };
    }

    // ── Register ──────────────────────────────────────────────────────────

    [Fact]
    public async Task Register_ReturnsOk_WhenRegistrationSucceeds()
    {
        // Arrange
        var request = new RegisterRequest { Email = "new@example.com", Password = "Pass123!", FirstName = "New", LastName = "User" };
        var response = BuildAuthResponse();

        _mediator.Send(Arg.Any<RegisterCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(response));

        // Act
        var result = await _controller.Register(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(response);

        await _mediator.Received(1).Send(
            Arg.Is<RegisterCommand>(c =>
                c.Email == request.Email &&
                c.FirstName == request.FirstName &&
                c.LastName == request.LastName),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Register_ReturnsProblem_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new RegisterRequest { Email = "existing@example.com", Password = "Pass123!", FirstName = "New", LastName = "User" };

        _mediator.Send(Arg.Any<RegisterCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<AuthResponse>(DomainErrors.Authentication.AlreadyExists));

        // Act
        var result = await _controller.Register(request, CancellationToken.None);

        // Assert
        var problem = result.Result.Should().BeOfType<ObjectResult>().Subject;
        var details = problem.Value.Should().BeOfType<ProblemDetails>().Subject;
        details.Status.Should().Be(DomainErrors.Authentication.AlreadyExists.Code);
        details.Title.Should().Be(DomainErrors.Authentication.AlreadyExists.Title);
        details.Detail.Should().Be(DomainErrors.Authentication.AlreadyExists.Message);
    }

    // ── Login ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task Login_ReturnsOk_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new LoginRequest { Email = "test@example.com", Password = "Pass123!" };
        var response = BuildAuthResponse();

        _mediator.Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(response));

        // Act
        var result = await _controller.Login(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task Login_ReturnsProblem_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new LoginRequest { Email = "test@example.com", Password = "WrongPass" };

        _mediator.Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<AuthResponse>(DomainErrors.Authentication.InvalidCredentials));

        // Act
        var result = await _controller.Login(request, CancellationToken.None);

        // Assert
        var details = result.Result.Should().BeOfType<ObjectResult>()
                             .Which.Value.Should().BeOfType<ProblemDetails>().Subject;
        details.Status.Should().Be(DomainErrors.Authentication.InvalidCredentials.Code);
    }

    // ── RefreshToken ──────────────────────────────────────────────────────

    [Fact]
    public async Task RefreshToken_ReturnsOk_WhenTokenIsValid()
    {
        // Arrange
        var request = new RefreshTokenRequest { RefreshToken = "valid-token" };
        var response = BuildAuthResponse();

        _mediator.Send(Arg.Any<RefreshTokenCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(response));

        // Act
        var result = await _controller.RefreshToken(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task RefreshToken_ReturnsProblem_WhenTokenIsInvalid()
    {
        // Arrange
        var request = new RefreshTokenRequest { RefreshToken = "invalid-token" };

        _mediator.Send(Arg.Any<RefreshTokenCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<AuthResponse>(DomainErrors.Authentication.InvalidRefreshToken));

        // Act
        var result = await _controller.RefreshToken(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.Authentication.InvalidRefreshToken.Code);
    }

    // ── RevokeToken ───────────────────────────────────────────────────────

    [Fact]
    public async Task RevokeToken_ReturnsNoContent_WhenTokenIsValid()
    {
        // Arrange
        var request = new RefreshTokenRequest { RefreshToken = "valid-token" };

        _mediator.Send(Arg.Any<RevokeTokenCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success());

        // Act
        var result = await _controller.RevokeToken(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        await _mediator.Received(1).Send(
            Arg.Is<RevokeTokenCommand>(c => c.RefreshToken == request.RefreshToken),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RevokeToken_ReturnsProblem_WhenTokenIsInvalid()
    {
        // Arrange
        var request = new RefreshTokenRequest { RefreshToken = "invalid-token" };

        _mediator.Send(Arg.Any<RevokeTokenCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure(DomainErrors.Authentication.InvalidRefreshToken));

        // Act
        var result = await _controller.RevokeToken(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.Authentication.InvalidRefreshToken.Code);
    }
}
