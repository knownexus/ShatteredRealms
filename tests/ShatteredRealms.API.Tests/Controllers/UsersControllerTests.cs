using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ShatteredRealms.API.Controllers;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Features.Users.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Tests.Controllers;

public sealed class UsersControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<UsersController> _logger = Substitute.For<ILogger<UsersController>>();
    private readonly UsersController _controller;

    private static UserDto BuildUserDto(string id = "user-1") => new()
    {
        Id = id,
        Email = "test@example.com",
        FirstName = "Test",
        LastName = "User",
        Roles = new List<string> { "User" },
    };

    public UsersControllerTests()
    {
        _controller = new UsersController(_mediator, _logger)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
        };
    }

    // ── GetAll ────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk_WithUserList()
    {
        // Arrange
        var users = new List<UserDto> { BuildUserDto("1"), BuildUserDto("2") };

        _mediator.Send(Arg.Any<GetAllUsersQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(users));

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task GetAll_ReturnsProblem_WhenQueryFails()
    {
        // Arrange
        _mediator.Send(Arg.Any<GetAllUsersQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<List<UserDto>>(DomainErrors.User.NotFound));

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>();
    }

    // ── GetById ───────────────────────────────────────────────────────────

    [Fact]
    public async Task GetById_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var user = BuildUserDto("user-1");

        _mediator.Send(Arg.Any<GetUserByIdQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(user));

        // Act
        var result = await _controller.GetById("user-1", CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetById_ReturnsProblem_WhenUserNotFound()
    {
        // Arrange
        _mediator.Send(Arg.Any<GetUserByIdQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<UserDto>(DomainErrors.User.NotFound));

        // Act
        var result = await _controller.GetById("missing-id", CancellationToken.None);

        // Assert
        var details = result.Result.Should().BeOfType<ObjectResult>()
                             .Which.Value.Should().BeOfType<ProblemDetails>().Subject;
        details.Status.Should().Be(DomainErrors.User.NotFound.Code);
    }

    // ── Create ────────────────────────────────────────────────────────────

    [Fact]
    public async Task Create_ReturnsCreated_WhenUserCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateUserRequest { Email = "new@example.com", Password = "Pass123!", FirstName = "New", LastName = "User" };
        var user = BuildUserDto("new-id");

        _mediator.Send(Arg.Any<CreateUserCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(user));

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>()
              .Which.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Create_ReturnsProblem_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new CreateUserRequest { Email = "existing@example.com", Password = "Pass123!", FirstName = "A", LastName = "B" };

        _mediator.Send(Arg.Any<CreateUserCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<UserDto>(DomainErrors.User.AlreadyExists));

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.User.AlreadyExists.Code);
    }

    // ── Delete ────────────────────────────────────────────────────────────

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenUserDeleted()
    {
        // Arrange
        _mediator.Send(Arg.Any<DeleteUserCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success());

        // Act
        var result = await _controller.Delete("user-1", CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        await _mediator.Received(1).Send(
            Arg.Is<DeleteUserCommand>(c => c.UserId == "user-1"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Delete_ReturnsProblem_WhenUserIsSystemAccount()
    {
        // Arrange
        _mediator.Send(Arg.Any<DeleteUserCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure(DomainErrors.User.CannotDeleteSystemUser));

        // Act
        var result = await _controller.Delete("system-id", CancellationToken.None);

        // Assert
        result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.User.CannotDeleteSystemUser.Code);
    }
}
