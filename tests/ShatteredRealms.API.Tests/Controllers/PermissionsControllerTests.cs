using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ShatteredRealms.API.Controllers;
using ShatteredRealms.Application.DTOs.Permissions;
using ShatteredRealms.Application.Features.Permissions.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Tests.Controllers;

public sealed class PermissionsControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<PermissionsController> _logger = Substitute.For<ILogger<PermissionsController>>();
    private readonly PermissionsController _controller;

    public PermissionsControllerTests()
    {
        _controller = new PermissionsController(_mediator, _logger)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
        };
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithPermissionList()
    {
        // Arrange
        var permissions = new List<PermissionDto>
        {
            new() { Id = 1, Name = "Users.View", Description = "View users", Category = "Users" },
            new() { Id = 2, Name = "Users.Create", Description = "Create users", Category = "Users" },
        };

        _mediator.Send(Arg.Any<GetAllPermissionsQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Success(permissions));

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(permissions);
    }

    [Fact]
    public async Task GetAll_ReturnsProblem_WhenNoPermissionsExist()
    {
        // Arrange
        _mediator.Send(Arg.Any<GetAllPermissionsQuery>(), Arg.Any<CancellationToken>())
                 .Returns(Result.Failure<List<PermissionDto>>(DomainErrors.Permissions.NoPermissions));

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ObjectResult>()
              .Which.Value.Should().BeOfType<ProblemDetails>()
              .Which.Status.Should().Be(DomainErrors.Permissions.NoPermissions.Code);
    }
}
