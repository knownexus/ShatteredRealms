using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Permissions;
using ShatteredRealms.Application.Features.Permissions.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PermissionsController> _logger;

    public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>Returns all permission definitions. Requires View permissions permission.</summary>
    [RequirePermission(Claims.Permissions.PermissionControl.View)]
    [HttpGet]
    public async Task<ActionResult<List<PermissionDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogDebug("GetAll permissions - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(new GetAllPermissionsQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }
}
