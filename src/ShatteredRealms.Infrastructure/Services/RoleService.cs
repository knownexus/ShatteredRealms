using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class RoleService : IRoleService
{
    private readonly ApplicationDbContext _context;

    public RoleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RoleDto>>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _context.Set<Role>()
            .Include(r => r.Permissions)
            .Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name!,
                Description = r.Description,
                PermissionIds = r.Permissions.Select(p => p.Id).ToList(),
            })
            .ToListAsync(cancellationToken);

        return Result.Create(roles);
    }

    public async Task<Result<RoleDto>> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var role = await _context.Set<Role>()
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);

        if (role == null)
        {
            return Result.Failure<RoleDto>(DomainErrors.Role.NotFound);
        }

        return Result.Create(new RoleDto
        {
            Id = role.Id,
            Name = role.Name!,
            Description = role.Description,
            PermissionIds = role.Permissions.Select(p => p.Id).ToList(),
        });
    }

    public async Task<Result<RoleDto>> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
    {
        var nameConflict = await _context.Set<Role>()
            .AnyAsync(r => r.Name == request.Name, cancellationToken);

        if (nameConflict)
        {
            return Result.Failure<RoleDto>(DomainErrors.Role.AlreadyExists);
        }

        var sourcePermissions = await _context.Set<Permission>()
            .Where(p => request.PermissionIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var role = new Role
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            NormalizedName = request.Name.ToUpper(),
            Description = request.Description,
            IsSystem = false,
        };

        _context.Set<Role>().Add(role);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var src in sourcePermissions)
        {
            _context.Set<Permission>().Add(new Permission
            {
                RoleId = role.Id,
                ClaimType = src.ClaimType,
                ClaimValue = src.ClaimValue,
                Description = src.Description,
                Category = src.Category,
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Create(new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            PermissionIds = sourcePermissions.Select(p => p.Id).ToList(),
        });
    }

    public async Task<Result<RoleDto>> UpdateRoleAsync(string roleId, UpdateRoleRequest request, CancellationToken cancellationToken = default)
    {
        var role = await _context.Set<Role>()
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);

        if (role == null)
        {
            return Result.Failure<RoleDto>(DomainErrors.Role.NotFound);
        }

        if (role.IsSystem)
        {
            return Result.Failure<RoleDto>(DomainErrors.Role.CannotModifySystemRole);
        }

        var nameConflict = await _context.Set<Role>()
            .AnyAsync(r => r.Name == request.Name && r.Id != roleId, cancellationToken);

        if (nameConflict)
        {
            return Result.Failure<RoleDto>(DomainErrors.Role.AlreadyExists);
        }

        var sourcePermissions = await _context.Set<Permission>()
            .Where(p => request.PermissionIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpper();
        role.Description = request.Description;

        _context.Set<Permission>().RemoveRange(role.Permissions);

        foreach (var src in sourcePermissions)
        {
            _context.Set<Permission>().Add(new Permission
            {
                RoleId = role.Id,
                ClaimType = src.ClaimType,
                ClaimValue = src.ClaimValue,
                Description = src.Description,
                Category = src.Category,
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Create(new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            PermissionIds = sourcePermissions.Select(p => p.Id).ToList(),
        });
    }

    public async Task<Result> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var role = await _context.Set<Role>().FindAsync(new object[] { roleId }, cancellationToken);
        if (role == null)
        {
            return Result.Failure(DomainErrors.Role.NotFound);
        }

        if (role.IsSystem)
        {
            return Result.Failure(DomainErrors.Role.CannotDeleteSystemRole);
        }

        _context.Set<Role>().Remove(role);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
