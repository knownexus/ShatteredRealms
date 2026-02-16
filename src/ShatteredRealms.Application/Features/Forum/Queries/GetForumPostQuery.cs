using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Queries;

/// <summary>Returns a single post by ID.</summary>
public sealed record GetForumPostQuery(int PostId) : IRequest<Result<ForumPostDto>>;
