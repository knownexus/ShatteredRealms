using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class CharactersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CharactersController(IMediator mediator) => _mediator = mediator;

    [RequirePermission(Claims.Permissions.Characters.ViewOwn)]
    [HttpGet("self")]
    public async Task<ActionResult<List<CharacterDto>>> GetMine(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new GetMyCharactersQuery(userId), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.View)]
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<CharacterDto>>> GetByUser(string userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCharactersByUserQuery(userId), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.View)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCharacterByIdQuery(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.CreateOwn)]
    [HttpPost]
    public async Task<ActionResult<CharacterDto>> Create([FromBody] CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new CreateCharacterCommand(userId, request.Name, request.Nationality), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.UpdateOwn)]
    [HttpPut("self/{id:int}")]
    public async Task<ActionResult<CharacterDto>> UpdateOwn(int id, [FromBody] UpdateCharacterRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new UpdateOwnCharacterCommand(id, userId, request.Name, request.Nationality), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.Update)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CharacterDto>> Update(int id, [FromBody] UpdateCharacterByAdminRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateCharacterCommand(id, request.Name, request.Nationality, request.Level, request.Experience), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.AssignExperience)]
    [HttpPut("{id:int}/assign-xp/{amount:int}")]
    public async Task<ActionResult<CharacterDto>> AssignXp(int id, int amount, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AssignExperienceCommand(id, amount), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Characters.DeleteOwn)]
    [HttpDelete("self/{id:int}")]
    public async Task<IActionResult> DeleteOwn(int id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Problem(detail: "User ID cannot be null or empty", statusCode: 400, title: "Invalid User ID");

        var result = await _mediator.Send(new DeleteOwnCharacterCommand(id, userId), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }

    [RequirePermission(Claims.Permissions.Characters.Delete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCharacterCommand(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }
}
