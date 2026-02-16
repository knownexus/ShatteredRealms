using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Permissions;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<PermissionDto>>> GetAllPermissionsAsync(CancellationToken cancellationToken = default)
    {
        var permissions = await _context.Set<Permission>()
                                        .AsNoTracking()
                                        .OrderBy(p => p.Id)
                                        .ToListAsync(cancellationToken);

        permissions = permissions
                     .DistinctBy(p => p.ClaimValue)
                     .ToList();

        var result = permissions.Select(p => new PermissionDto
        {
            Id = p.Id, Name = p.ClaimValue!, Description = p.Description, Category = p.Category
        }).ToList();

        return permissions.Count == 0
            ? Result.Failure<List<PermissionDto>>(DomainErrors.Permissions.NoPermissions)
            : Result.Create(result);
    }

    public async Task<Result<List<string>>> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var permissions = await _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.Permissions.Select(p => p.ClaimValue))
            .Distinct()
            .ToListAsync(cancellationToken);

        return Result.Create(permissions);
    }
}
