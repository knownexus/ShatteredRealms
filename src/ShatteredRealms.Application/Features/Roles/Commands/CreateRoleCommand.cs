using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Roles.Commands;

public sealed record CreateRoleCommand(
    string Name,
    string Description,
    List<int> PermissionIds) : IRequest<Result<RoleDto>>;
