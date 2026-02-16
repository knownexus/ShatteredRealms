using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Wiki.Commands;

/// <summary>Creates a new user account.</summary>
public sealed record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    List<string> RoleIds) : IRequest<Result<UserDto>>;
