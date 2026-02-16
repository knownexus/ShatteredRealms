using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleService _roleService;

    public UpdateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public Task<Result<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        => _roleService.UpdateRoleAsync(request.RoleId, new UpdateRoleRequest
        {
            Name = request.Name,
            Description = request.Description,
            PermissionIds = request.PermissionIds,
        }, cancellationToken);
}
