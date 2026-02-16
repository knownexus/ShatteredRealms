using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.API.Authorization;
using ShatteredRealms.API.Extensions;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Features.Forum.Commands;
using ShatteredRealms.Application.Features.Forum.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class ForumController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ForumController> _logger;

    public ForumController(IMediator mediator, ILogger<ForumController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    // ── Categories ─────────────────────────────────────────────────────────

    /// <summary>Returns all forum categories. Any authenticated user.</summary>
    [HttpGet("categories")]
    public async Task<ActionResult<List<ForumCategoryDto>>> GetCategories(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetForumCategoriesQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a single category. Any authenticated user.</summary>
    [HttpGet("categories/{id:int}")]
    public async Task<ActionResult<ForumCategoryDto>> GetCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetForumCategoryQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Creates a forum category. Requires Create Category claim.</summary>
    [RequirePermission(Claims.Permissions.Forum.Category.Create)]
    [HttpPost("categories")]
    public async Task<ActionResult<ForumCategoryDto>> CreateCategory(
        [FromBody] CreateForumCategoryRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create forum category - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(
            new CreateForumCategoryCommand(User.GetUserId(), request.Name, request.Description, request.SortOrder),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return CreatedAtAction(nameof(GetCategory), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>Updates a forum category. Requires Update Category claim.</summary>
    [RequirePermission(Claims.Permissions.Forum.Category.Update)]
    [HttpPut("categories/{id:int}")]
    public async Task<ActionResult<ForumCategoryDto>> UpdateCategory(
        int id,
        [FromBody] UpdateForumCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UpdateForumCategoryCommand(id, request.Name, request.Description, request.SortOrder),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Deletes a forum category. Requires Delete Category claim.</summary>
    [RequirePermission(Claims.Permissions.Forum.Category.Delete)]
    [HttpDelete("categories/{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteForumCategoryCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return NoContent();
    }

    // ── Threads ────────────────────────────────────────────────────────────

    /// <summary>Returns all threads in a category. Any authenticated user.</summary>
    [HttpGet("categories/{categoryId:int}/threads")]
    public async Task<ActionResult<List<ForumThreadDto>>> GetThreads(int categoryId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetForumThreadsQuery(categoryId), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a single thread. Any authenticated user.</summary>
    [HttpGet("threads/{id:int}")]
    public async Task<ActionResult<ForumThreadDto>> GetThread(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetForumThreadQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Creates a new thread with an opening post. Any authenticated user.</summary>
    [HttpPost("threads")]
    public async Task<ActionResult<ForumThreadDto>> CreateThread(
        [FromBody] CreateForumThreadRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create forum thread - UserId: {UserId}", User.GetUserId());

        var result = await _mediator.Send(
            new CreateForumThreadCommand(User.GetUserId(), request.CategoryId, request.Title, request.InitialPostContent),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return CreatedAtAction(nameof(GetThread), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>Updates thread metadata (title, pinned, locked). Requires Update Thread claim.</summary>
    [RequirePermission(Claims.Permissions.Forum.Thread.Update)]
    [HttpPut("threads/{id:int}")]
    public async Task<ActionResult<ForumThreadDto>> UpdateThread(
        int id,
        [FromBody] UpdateForumThreadRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UpdateForumThreadCommand(id, User.GetUserId(), request.Title, request.IsPinned, request.IsLocked),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Soft-deletes a thread. Requires Delete Thread claim.</summary>
    [RequirePermission(Claims.Permissions.Forum.Thread.Delete)]
    [HttpDelete("threads/{id:int}")]
    public async Task<IActionResult> DeleteThread(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteForumThreadCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return NoContent();
    }

    // ── Posts ──────────────────────────────────────────────────────────────

    /// <summary>Returns all posts in a thread. Any authenticated user.</summary>
    [HttpGet("threads/{threadId:int}/posts")]
    public async Task<ActionResult<List<ForumPostDto>>> GetPosts(int threadId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetForumPostsQuery(threadId), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Returns a single post. Any authenticated user.</summary>
    [HttpGet("posts/{id:int}")]
    public async Task<ActionResult<ForumPostDto>> GetPost(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetForumPostQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Adds a reply to an unlocked thread. Any authenticated user.</summary>
    [HttpPost("posts")]
    public async Task<ActionResult<ForumPostDto>> CreatePost(
        [FromBody] CreateForumPostRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new CreateForumPostCommand(User.GetUserId(), request.ThreadId, request.Content),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return CreatedAtAction(nameof(GetPost), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>Edits a post. The requesting user must be the post's author.</summary>
    [HttpPut("posts/{id:int}")]
    public async Task<ActionResult<ForumPostDto>> UpdatePost(
        int id,
        [FromBody] UpdateForumPostRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UpdateForumPostCommand(id, User.GetUserId(), request.Content),
            cancellationToken);

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return Ok(result.Value);
    }

    /// <summary>Soft-deletes a post. Author or Admin.</summary>
    [HttpDelete("posts/{postId:int}")]
    public async Task<IActionResult> DeletePost(int postId, CancellationToken cancellationToken)
    {
        var role = User.GetUserRole();
        Result result;

        if (role == Claims.Roles.AdminName || role == Claims.Roles.SystemName)
        {
          result = await _mediator.Send(new DeleteForumPostAsAdminCommand(postId), cancellationToken);
        }
        else
        {
          result = await _mediator.Send(new DeleteForumPostCommand(postId, User.GetUserId()),
                                               cancellationToken);
        }

        if (result.IsFailure)
        {
            return Problem(detail: result.Error.Message, statusCode: result.Error.Code, title: result.Error.Title);
        }

        return NoContent();
    }
}
