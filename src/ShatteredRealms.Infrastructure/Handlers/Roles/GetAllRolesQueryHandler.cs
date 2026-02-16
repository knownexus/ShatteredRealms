using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleDto>>>
{
    private readonly IRoleService _roleService;

    public GetAllRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public Task<Result<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        => _roleService.GetAllRolesAsync(cancellationToken);
}
