using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Commands;

/// <summary>Updates an existing user's profile and role assignments.</summary>
public sealed record UpdateUserCommand(
    string UserId,
    string Email,
    string FirstName,
    string LastName,
    List<string> RoleIds) : IRequest<Result<UserDto>>;
