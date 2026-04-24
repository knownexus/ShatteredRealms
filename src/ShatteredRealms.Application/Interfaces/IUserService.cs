using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Interfaces;

public interface IUserService
{
    Task<Result<List<User>>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<Result<User>> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Result<User>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<Result<User>> UpdateUserAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<List<string>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default);
    Task<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
    Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
}
