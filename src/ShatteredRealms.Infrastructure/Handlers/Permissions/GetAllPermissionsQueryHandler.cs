using MediatR;
using ShatteredRealms.Application.DTOs.Permissions;
using ShatteredRealms.Application.Features.Permissions.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Permissions;

public sealed class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Result<List<PermissionDto>>>
{
    private readonly IPermissionService _permissionService;

    public GetAllPermissionsQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public Task<Result<List<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        => _permissionService.GetAllPermissionsAsync(cancellationToken);
}
