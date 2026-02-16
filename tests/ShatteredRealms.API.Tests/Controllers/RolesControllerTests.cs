using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ShatteredRealms.API.Controllers;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Features.Roles.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Tests.Controllers;

public sealed class RolesControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<RolesController> _logger = Substitute.For<ILogger<RolesController>>();
    private readonly RolesController _controller;

    private static RoleDto BuildRoleDto(string id = "role-1", string name = "TestRole") => new()
    {
        Id = id,
        Name = name,
        Description = "A test role",
        PermissionIds = new List<int>(),
    };

    public RolesControllerTests()
    {
        _controller = new RolesController(_mediator, _logger)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
        };
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithRoleList()
    {
        // Arrange
        var roles = new List<RoleDto> { BuildRoleDto("1", "Admin"), BuildRoleDto("2", "User") };

        _mediator.Send(Arg.Any<GetAllRolesQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(roles));

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(roles);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenRoleExists()
    {
        // Arrange
        var role = BuildRoleDto();

        _mediator.Send(Arg.Any<GetRoleByIdQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(role));

        // Act
        var result = await _controller.GetById("role-1", CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(role);
    }

    [Fact]
    public async Task GetById_ReturnsProblem_WhenRoleNotFound()
    {
        // Arrange
        _mediator.Send(Arg.Any<GetRoleByIdQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<RoleDto>(DomainErrors.Role.NotFound));

        // Act
        var result = await _controller.GetById("missing", CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.Role.NotFound.Code);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenRoleCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateRoleRequest { Name = "NewRole", Description = "Desc", PermissionIds = new List<int>() };
        var role = BuildRoleDto("new-role-id", "NewRole");

        _mediator.Send(Arg.Any<CreateRoleCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(role));

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>()
              .Which.Value.Should().BeEquivalentTo(role);
    }

    [Fact]
    public async Task Create_ReturnsProblem_WhenNameAlreadyExists()
    {
        // Arrange
        var request = new CreateRoleRequest { Name = "Existing", Description = "Desc", PermissionIds = new List<int>() };

        _mediator.Send(Arg.Any<CreateRoleCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<RoleDto>(DomainErrors.Role.AlreadyExists));

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.Role.AlreadyExists.Code);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenRoleDeleted()
    {
        // Arrange
        _mediator.Send(Arg.Any<DeleteRoleCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success());

        // Act
        var result = await _controller.Delete("role-1", CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_ReturnsProblem_WhenRoleIsSystemRole()
    {
        // Arrange
        _mediator.Send(Arg.Any<DeleteRoleCommand>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure(DomainErrors.Role.CannotDeleteSystemRole));

        // Act
        var result = await _controller.Delete("system-role-id", CancellationToken.None);

        // Assert
        result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.Role.CannotDeleteSystemRole.Code);
    }
}
