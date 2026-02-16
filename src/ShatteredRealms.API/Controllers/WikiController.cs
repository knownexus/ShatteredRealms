using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Features.Wiki.Commands;
using ShatteredRealms.Application.Features.Wiki.Queries;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class WikiController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WikiController> _logger;

    public WikiController(IMediator mediator, ILogger<WikiController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    // ── Pages ──────────────────────────────────────────────────────────────

    /// <summary>Returns all wiki pages, optionally filtered by category. Any authenticated user.</summary>
    [HttpGet("pages")]
    public async Task<ActionResult<WikiPagesDto>> GetPages(
        [FromQuery] int? categoryId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWikiPagesQuery(categoryId), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a single wiki page by ID. Any authenticated user.</summary>
    [HttpGet("pages/{id:int}")]
    public async Task<ActionResult<WikiPageDto>> GetPage(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWikiPageQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a wiki page by URL slug. Any authenticated user.</summary>
    [HttpGet("pages/slug/{slug}")]
    public async Task<ActionResult<WikiPageDto>> GetPageBySlug(string slug, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWikiPageBySlugQuery(slug), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Creates a wiki page with an initial revision. Any authenticated user.</summary>
    [HttpPost("pages")]
    public async Task<ActionResult<WikiPageDto>> CreatePage(
        [FromBody] CreateWikiPageRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create wiki page - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(
            new CreateWikiPageCommand(User.GetUserId(), request.Title, request.Content, request.RevisionNote, request.CategoryIds),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return CreatedAtAction(nameof(GetPage), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>Updates a wiki page and records a revision snapshot. Any authenticated user.</summary>
    [HttpPut("pages/{id:int}")]
    public async Task<ActionResult<WikiPageDto>> UpdatePage(
        int id,
        [FromBody] UpdateWikiPageRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update wiki page {PageId} - UserId: {UserId}", id, User.GetUserId());

        var result = await _mediator.Send(
            new UpdateWikiPageCommand(id, User.GetUserId(), request.Title, request.Content, request.RevisionNote, request.CategoryIds),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Soft-deletes a wiki page. Requires Delete Page claim.</summary>
    [RequirePermission(Claims.Permissions.Wiki.Page.Delete)]
    [HttpDelete("pages/{id:int}")]
    public async Task<IActionResult> DeletePage(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete wiki page {PageId} - UserId: {UserId}", id, User.GetUserId());

        var result = await _mediator.Send(new DeleteWikiPageCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return NoContent();
    }

    // ── Revisions ──────────────────────────────────────────────────────────

    /// <summary>Returns all revisions for a page, most recent first. Any authenticated user.</summary>
    [HttpGet("pages/{pageId:int}/revisions")]
    public async Task<ActionResult<WikiRevisionsDto>> GetRevisions(int pageId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWikiRevisionsQuery(pageId), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    // ── Categories ─────────────────────────────────────────────────────────

    /// <summary>Returns all wiki categories. Any authenticated user.</summary>
    [HttpGet("categories")]
    public async Task<ActionResult<WikiCategoriesDto>> GetCategories(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetWikiCategoriesQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Creates a wiki category. Requires Manage Category claim.</summary>
    [RequirePermission(Claims.Permissions.Wiki.Category.Manage)]
    [HttpPost("categories")]
    public async Task<ActionResult<WikiCategoryDto>> CreateCategory(
        [FromBody] CreateWikiCategoryRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create wiki category - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(
            new CreateWikiCategoryCommand(request.Name, request.Description),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }
}
