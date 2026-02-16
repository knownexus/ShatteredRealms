using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Soft-deletes a forum thread. Admin only.</summary>
public sealed record DeleteForumThreadCommand(int ThreadId) : IRequest<Result>;
