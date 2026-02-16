using ShatteredRealms.Application.DTOs.Permissions;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Interfaces;

public interface IPermissionService
{
    Task<Result<List<PermissionDto>>> GetAllPermissionsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<string>>> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken = default);
}
