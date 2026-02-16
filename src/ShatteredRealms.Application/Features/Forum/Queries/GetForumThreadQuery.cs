using MediatR;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Queries;

/// <summary>Returns a single forum thread by ID.</summary>
public sealed record GetForumThreadQuery(int ThreadId) : IRequest<Result<ForumThreadDto>>;
