using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Roles.Commands;

public sealed record DeleteRoleCommand(string RoleId, string? ActorId = null) : IRequest<Result>;
