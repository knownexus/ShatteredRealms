using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Features.Roles.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class RolesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IMediator mediator, ILogger<RolesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>Returns all roles. Requires View role permission.</summary>
    [RequirePermission(Claims.Permissions.Roles.View)]
    [HttpGet]
    public async Task<ActionResult<List<RoleDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogDebug("GetAll roles - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a single role by ID. Requires View role permission.</summary>
    [RequirePermission(Claims.Permissions.Roles.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Creates a new role. Requires Create role permission.</summary>
    [RequirePermission(Claims.Permissions.Roles.Create)]
    [HttpPost]
    public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Create role - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(
            new CreateRoleCommand(request.Name, request.Description, request.PermissionIds),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        _logger.LogDebug("Role created - RoleId: {RoleId}", result.Value.Id);
        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>Updates an existing role. Requires Update role permission.</summary>
    [RequirePermission(Claims.Permissions.Roles.Update)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RoleDto>> Update(string id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UpdateRoleCommand(id, request.Name, request.Description, request.PermissionIds),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Deletes a role.  Requires Delete role permission.</summary>
    [RequirePermission(Claims.Permissions.Roles.Delete)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Delete role {RoleId} - UserId: {UserId}", id, User.GetUserId());

        var result = await _mediator.Send(new DeleteRoleCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return NoContent();
    }
}
