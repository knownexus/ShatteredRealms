using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NSubstitute;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Application.Settings;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Auth;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Domain.Entities.User;

namespace ShatteredRealms.Application.Tests.Handlers.Auth;

public sealed class RegisterCommandHandlerTests
{
    private readonly IUserService _userService = Substitute.For<IUserService>();
    private readonly IEmailService _emailService = Substitute.For<IEmailService>();
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
    private readonly ApplicationDbContext _context;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        var confirmationSettings = Substitute.For<IOptionsMonitor<ConfirmationSettings>>();
        confirmationSettings.CurrentValue.Returns(new ConfirmationSettings { RequireEmailConfirmation = false });
        confirmationSettings.Get(Arg.Any<string>()).Returns(new ConfirmationSettings { RequireEmailConfirmation = false });
        _handler = new RegisterCommandHandler(_userService, _emailService, _configuration, confirmationSettings);
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenRegistrationSucceeds()
    {
        // Arrange
        var command = new RegisterCommand("new@example.com", "Pass123!", "Jane", "Doe");
        var user = new User { Id = "user-1", Email = "new@example.com", UserName = "new@example.com", FirstName = "Jane", LastName = "Doe" };

        _userService.CreateUserAsync(Arg.Any<CreateUserRequest>(), Arg.Any<CancellationToken>())
                    .Returns(Result.Success(user));
        _userService.GetUserRolesAsync(user.Id, Arg.Any<CancellationToken>())
                    .Returns(Result.Success(new List<string> { "User" }));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Message.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new RegisterCommand("existing@example.com", "Pass123!", "Jane", "Doe");

        _userService.CreateUserAsync(Arg.Any<CreateUserRequest>(), Arg.Any<CancellationToken>())
                    .Returns(Result.Failure<User>(DomainErrors.User.AlreadyExists));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.AlreadyExists);
    }

    // Uses FakeOptionsMonitor from LoginCommandHandlerTests
}
