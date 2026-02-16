using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Queries;

/// <summary>Returns all non-deleted posts in a thread, ordered oldest-first.</summary>
public sealed record GetForumPostsQuery(int ThreadId) : IRequest<Result<List<ForumPostDto>>>;
