using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        => _roleService.CreateRoleAsync(new CreateRoleRequest
        {
            Name = request.Name,
            Description = request.Description,
            PermissionIds = request.PermissionIds,
        }, cancellationToken);
}
