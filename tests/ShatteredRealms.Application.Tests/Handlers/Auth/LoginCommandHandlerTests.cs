using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Settings;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Auth;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Domain.Entities.User;

namespace ShatteredRealms.Application.Tests.Handlers.Auth;

public sealed class LoginCommandHandlerTests
{
    private readonly IUserService _userService = Substitute.For<IUserService>();
    private readonly ITokenService _tokenService = Substitute.For<ITokenService>();
    private readonly IPermissionService _permissionService = Substitute.For<IPermissionService>();
    private readonly ApplicationDbContext _context;
    private readonly LoginCommandHandler _handler;

    private static User BuildValidUser() => new()
    {
        Id = "user-1",
        Email = "test@example.com",
        UserName = "test@example.com",
        FirstName = "Test",
        LastName = "User",
    };

    public LoginCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        var confirmationSettings = Substitute.For<IOptionsMonitor<ConfirmationSettings>>();
        confirmationSettings.CurrentValue.Returns(new ConfirmationSettings { RequireEmailConfirmation = false });
        confirmationSettings.Get(Arg.Any<string>()).Returns(new ConfirmationSettings { RequireEmailConfirmation = false });
        _handler = new LoginCommandHandler(_userService, _tokenService, _permissionService, _context, confirmationSettings);
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenCredentialsAreValid()
    {
        // Arrange
        var command = new LoginCommand("test@example.com", "Pass123!");
        var user = BuildValidUser();

        _userService.GetUserByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                    .Returns(Result.Success(user));
        _userService.CheckPasswordAsync(user, command.Password)
                    .Returns(true);
        _userService.GetUserRolesAsync(user.Id, Arg.Any<CancellationToken>())
                    .Returns(Result.Success(new List<string> { "User" }));
        _permissionService.GetUserPermissionsAsync(user.Id, Arg.Any<CancellationToken>())
                          .Returns(Result.Create(new List<string>()));
        _tokenService.GenerateAccessToken(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>(), Arg.Any<List<string>>())
                     .Returns("access-token");
        _tokenService.GenerateRefreshToken()
                     .Returns("refresh-token");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.User.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task Handle_ReturnsInvalidCredentials_WhenEmailNotFound()
    {
        // Arrange
        var command = new LoginCommand("ghost@example.com", "Pass123!");

        _userService.GetUserByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                    .Returns(Result.Failure<User>(DomainErrors.User.NotFound));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        // Must return the generic credentials error - not the specific NotFound error (prevents email enumeration)
        result.Error.Should().Be(DomainErrors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Handle_ReturnsInvalidCredentials_WhenPasswordIsWrong()
    {
        // Arrange
        var command = new LoginCommand("test@example.com", "WrongPass");
        var user = BuildValidUser();

        _userService.GetUserByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                    .Returns(Result.Success(user));
        _userService.CheckPasswordAsync(user, command.Password)
                    .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Authentication.InvalidCredentials);
    }
}
