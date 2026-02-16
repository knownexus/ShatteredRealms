using FluentAssertions;
using NSubstitute;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Handlers.Users;

namespace ShatteredRealms.Application.Tests.Handlers.Users;

public sealed class CreateUserCommandHandlerTests
{
    private readonly IUserService _userService = Substitute.For<IUserService>();
    private readonly IPermissionService _permissionService = Substitute.For<IPermissionService>();
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _handler = new CreateUserCommandHandler(_userService, _permissionService);
    }

    [Fact]
    public async Task Handle_ReturnsUserDto_WhenCreateSucceeds()
    {
        // Arrange
        var command = new CreateUserCommand("new@example.com", "Pass123!", "New", "User", new List<string>());
        var user = new User { Id = "user-1", Email = "new@example.com", FirstName = "New", LastName = "User" };

        _userService.CreateUserAsync(Arg.Any<CreateUserRequest>(), Arg.Any<CancellationToken>())
                    .Returns(Result.Success(user));
        _userService.GetUserRolesAsync(user.Id, Arg.Any<CancellationToken>())
                    .Returns(Result.Success(new List<string> { "User" }));
        _permissionService.GetUserPermissionsAsync(user.Id, Arg.Any<CancellationToken>())
                          .Returns(Result.Create(new List<string>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(user.Email);
        result.Value.FirstName.Should().Be(user.FirstName);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new CreateUserCommand("taken@example.com", "Pass123!", "A", "B", new List<string>());

        _userService.CreateUserAsync(Arg.Any<CreateUserRequest>(), Arg.Any<CancellationToken>())
                    .Returns(Result.Failure<User>(DomainErrors.User.AlreadyExists));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.AlreadyExists);
    }
}
