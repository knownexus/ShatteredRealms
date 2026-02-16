using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Queries;

/// <summary>Returns all threads within a category, pinned threads first.</summary>
public sealed record GetForumThreadsQuery(int CategoryId) : IRequest<Result<List<ForumThreadDto>>>;
