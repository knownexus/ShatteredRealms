using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Positions;
using ShatteredRealms.Application.Features.Positions.Queries;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<PositionDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllPositionsQuery(), cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title)
            : Ok(result.Value);
    }
}
