using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IPermissionService _permissionService;

    public UserService(
        UserManager<User> userManager,
        ApplicationDbContext context,
        IPermissionService permissionService)
    {
        _userManager = userManager;
        _context = context;
        _permissionService = permissionService;
    }

    public async Task<Result<User>> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<User>(DomainErrors.User.NotFound);
        }

        var result = User.Validate(user);
        return result.IsFailure ? Result.Failure<User>(result.Error) : user;
    }

    public async Task<Result<List<User>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users
                                      .Include(u => u.UserRoles)
                                      .ThenInclude(ur => ur.Role)
                                      .ToListAsync(cancellationToken);

        foreach (var result in users.Select(user => User.Validate(user)).Where(result => result.IsFailure))
        {
            return Result.Failure<List<User>>(result.Error);
        }

        return Result.Success(users);
    }

    public async Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.Failure<User>(DomainErrors.User.NotFound);
        }

        var result = User.Validate(user);
        return result.IsFailure ? Result.Failure<User>(result.Error) : user;
    }

    public async Task<Result<User>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Result.Failure<User>(DomainErrors.User.AlreadyExists);
        }

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var validationResult = User.Validate(user);
        if (validationResult.IsFailure)
        {
            return Result.Failure<User>(validationResult.Error);
        }

        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            var description = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            return Result.Failure<User>(new Error("User.CreateFailed", description, (int)HttpStatusCode.UnprocessableEntity));
        }

        if (request.RoleIds is { Count: > 0 })
        {
            // Admin-created user with explicit roles — skip Unverified, assign those directly
            var assignResult = await AssignRolesToUserAsync(user.Id, request.RoleIds, cancellationToken);
            if (assignResult.IsFailure)
            {
                return Result.Failure<User>(assignResult.Error);
            }
        }
        else
        {
            // Self-registered user — starts as Unverified until an admin approves them
            _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = Claims.Roles.UnverifiedId });
        }

        await _context.SaveChangesAsync(cancellationToken);

        var createdUser = await _userManager.FindByIdAsync(user.Id);
        return createdUser != null
            ? Result.Success(createdUser)
            : Result.Failure<User>(DomainErrors.User.NotFound);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
        => await _userManager.CheckPasswordAsync(user, password);

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken = default)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user);

    public async Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            var description = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(new Error("Password.ChangeFailed", description, (int)System.Net.HttpStatusCode.BadRequest));
        }

        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            var description = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(new Error("EmailConfirmation.Failed", description, (int)System.Net.HttpStatusCode.BadRequest));
        }

        return Result.Success();
    }

    public async Task<Result<User>> UpdateUserAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        if (request.RoleIds.Any(id => id == Claims.Roles.SystemId))
        {
            return Result.Failure<User>(DomainErrors.Role.CannotAssignSystemRole);
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<User>(DomainErrors.User.NotFound);
        }

        var existingUserRoles = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .ToListAsync(cancellationToken);

        if (existingUserRoles.Any(ur => ur.RoleId == Claims.Roles.SystemId))
        {
            return Result.Failure<User>(DomainErrors.User.CannotModifySystemUser);
        }

        user.Email = request.Email;
        user.UserName = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        var validationResult = User.Validate(user);
        if (validationResult.IsFailure)
        {
            return Result.Failure<User>(validationResult.Error);
        }

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            var description = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            return Result.Failure<User>(new Error("User.UpdateFailed", description, (int)HttpStatusCode.UnprocessableEntity));
        }

        _context.UserRoles.RemoveRange(existingUserRoles);

        var assignResult = await AssignRolesToUserAsync(user.Id, request.RoleIds, cancellationToken);
        if (assignResult.IsFailure)
        {
            return Result.Failure<User>(assignResult.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var updatedUser = await _userManager.FindByIdAsync(userId);
        return updatedUser != null
            ? Result.Success(updatedUser)
            : Result.Failure<User>(DomainErrors.User.NotFound);
    }

    public async Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var existingUserRoles = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .ToListAsync(cancellationToken);

        if (existingUserRoles.Any(ur => ur.RoleId == Claims.Roles.SystemId))
        {
            return Result.Failure(DomainErrors.User.CannotDeleteSystemUser);
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var description = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(new Error("User.DeleteFailed", description, (int)HttpStatusCode.UnprocessableEntity));
        }

        return Result.Success();
    }

    public async Task<Result<List<string>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default)
    {
        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role.Name!)
            .ToListAsync(cancellationToken);

        return Result.Create(roles);
    }

    public async Task<Result<List<User>>> GetPendingUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == Claims.Roles.UnverifiedId) && u.EmailConfirmed)
            .ToListAsync(cancellationToken);

        return Result.Success(users);
    }

    public async Task<Result<User>> ApproveUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<User>(DomainErrors.User.NotFound);
        }

        var unverifiedRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == Claims.Roles.UnverifiedId, cancellationToken);

        if (unverifiedRole == null)
        {
            return Result.Failure<User>(DomainErrors.User.NotPendingApproval);
        }

        _context.UserRoles.Remove(unverifiedRole);
        _context.UserRoles.Add(new UserRole { UserId = userId, RoleId = Claims.Roles.UserId });
        await _context.SaveChangesAsync(cancellationToken);

        var updatedUser = await _userManager.FindByIdAsync(userId);
        return updatedUser != null
            ? Result.Success(updatedUser)
            : Result.Failure<User>(DomainErrors.User.NotFound);
    }

    private async Task<Result> AssignRolesToUserAsync(string userId, List<string> roleIds, CancellationToken cancellationToken)
    {
        if (roleIds.Count == 0)
        {
            return Result.Success();
        }

        var validRoleIds = await _context.Roles
            .Where(r => roleIds.Contains(r.Id))
            .Select(r => r.Id)
            .ToListAsync(cancellationToken);

        if (validRoleIds.Count == 0)
        {
            return Result.Failure(DomainErrors.Role.NotFound);
        }

        foreach (var roleId in validRoleIds)
        {
            var alreadyAssigned = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

            if (!alreadyAssigned)
            {
                _context.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
