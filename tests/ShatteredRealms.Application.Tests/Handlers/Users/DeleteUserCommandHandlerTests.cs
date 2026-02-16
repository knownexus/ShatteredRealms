using FluentAssertions;
using NSubstitute;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Handlers.Users;

namespace ShatteredRealms.Application.Tests.Handlers.Users;

public sealed class DeleteUserCommandHandlerTests
{
    private readonly IUserService _userService = Substitute.For<IUserService>();
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _handler = new DeleteUserCommandHandler(_userService);
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenUserDeleted()
    {
        // Arrange
        _userService.DeleteUserAsync("user-1", Arg.Any<CancellationToken>())
                    .Returns(Result.Success());

        // Act
        var result = await _handler.Handle(new DeleteUserCommand("user-1"), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _userService.Received(1).DeleteUserAsync("user-1", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenUserNotFound()
    {
        // Arrange
        _userService.DeleteUserAsync("missing", Arg.Any<CancellationToken>())
                    .Returns(Result.Failure(DomainErrors.User.NotFound));

        // Act
        var result = await _handler.Handle(new DeleteUserCommand("missing"), CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.NotFound);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenAttemptingToDeleteSystemUser()
    {
        // Arrange
        _userService.DeleteUserAsync("system-user-id", Arg.Any<CancellationToken>())
                    .Returns(Result.Failure(DomainErrors.User.CannotDeleteSystemUser));

        // Act
        var result = await _handler.Handle(new DeleteUserCommand("system-user-id"), CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.CannotDeleteSystemUser);
    }
}
