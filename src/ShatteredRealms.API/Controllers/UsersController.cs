using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Features.Users.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>Returns all users. Requires view users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.View)]
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAll users - requested by UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        _logger.LogInformation("GetAll users returned {Count} records", result.Value.Count);
        return Ok(result.Value);
    }

    /// <summary>Returns a single user by ID. Requires view users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id, CancellationToken cancellationToken)
    {
        _logger.LogDebug("GetById user {TargetUserId} - requested by UserId: {UserId}", id, User.GetUserId());

        var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a single user by ID. Requires view users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.ViewOwn)]
    [HttpGet("self")]
    public async Task<ActionResult<UserDto>> GetOwn(CancellationToken cancellationToken)
    {
        var callingUser = User.GetUserId();
        if (string.IsNullOrEmpty(callingUser))
        {
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");
        }
        _logger.LogDebug("GetOwn user - requested by UserId: {UserId}", callingUser);

        var result = await _mediator.Send(new GetUserByIdQuery(callingUser), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }
    
    /// <summary>Creates a new user, Requires Create users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.Create)]
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create user - requested by UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(
            new CreateUserCommand(request.Email, request.Password, request.FirstName, request.LastName, request.RoleIds),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        _logger.LogInformation("User created - new UserId: {NewUserId}", result.Value.Id);
        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>Updates calling user. Requires Update own users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.UpdateOwn)]
    [HttpPut("self")]
    public async Task<ActionResult<UserDto>> UpdateOwn([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var callingUser = User.GetUserId();
        if (string.IsNullOrEmpty(callingUser))
        {
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");
        }

        _logger.LogDebug("UpdateOwn user - requested by UserId: {UserId}", callingUser);

        var result = await _mediator.Send(new UpdateUserCommand(callingUser, request.Email, request.FirstName, request.LastName, request.RoleIds), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Updates an existing user. Requires Update users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.Update)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update user {TargetUserId} - requested by UserId: {UserId}", id, User.GetUserId());

        var result = await _mediator.Send(
            new UpdateUserCommand(id, request.Email, request.FirstName, request.LastName, request.RoleIds),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Deletes a user account. Requires Delete users permission.</summary>
    [RequirePermission(Claims.Permissions.Users.Delete)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete user {TargetUserId} - requested by UserId: {UserId}", id, User.GetUserId());

        var result = await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        _logger.LogInformation("User {TargetUserId} deleted", id);
        return NoContent();
    }
}
