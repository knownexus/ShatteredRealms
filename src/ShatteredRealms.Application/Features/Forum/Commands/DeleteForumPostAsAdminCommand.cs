using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Soft-deletes a forum post. Author or Admin.</summary>
public sealed record DeleteForumPostAsAdminCommand(int PostId) : IRequest<Result>;