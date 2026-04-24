using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Application.Features.Announcements.Commands;
using ShatteredRealms.Application.Features.Announcements.Queries;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class AnnouncementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnnouncementsController(IMediator mediator) => _mediator = mediator;

    [RequirePermission(Claims.Permissions.Announcements.View)]
    [HttpGet]
    public async Task<ActionResult<List<AnnouncementDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllAnnouncementsQuery(), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Announcements.View)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AnnouncementDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAnnouncementByIdQuery(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Announcements.Create)]
    [HttpPost]
    public async Task<ActionResult<AnnouncementDto>> Create(
        [FromBody] CreateAnnouncementRequest request,
        CancellationToken cancellationToken)
    {
        var authorId = User.GetUserId();
        if (string.IsNullOrEmpty(authorId))
            return Problem(detail: "User ID cannot be resolved", statusCode: 400, title: "Invalid User");

        var result = await _mediator.Send(
            new CreateAnnouncementCommand(authorId, request.Title, request.Body, request.LinkedEventId),
            cancellationToken);

        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [RequirePermission(Claims.Permissions.Announcements.Update)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<AnnouncementDto>> Update(
        int id,
        [FromBody] UpdateAnnouncementRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UpdateAnnouncementCommand(id, request.Title, request.Body, request.LinkedEventId),
            cancellationToken);

        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }

    [RequirePermission(Claims.Permissions.Announcements.Delete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteAnnouncementCommand(id), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : NoContent();
    }
}
