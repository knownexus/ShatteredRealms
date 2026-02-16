using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Interfaces;

public interface IRoleService
{
    Task<Result<List<RoleDto>>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<Result<RoleDto>> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default);
    Task<Result<RoleDto>> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
    Task<Result<RoleDto>> UpdateRoleAsync(string roleId, UpdateRoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default);
}
