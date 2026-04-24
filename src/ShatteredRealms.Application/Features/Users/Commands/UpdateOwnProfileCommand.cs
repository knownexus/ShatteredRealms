using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Commands;

/// <summary>Updates the calling user's own name and email, preserving their existing role assignments.</summary>
public sealed record UpdateOwnProfileCommand(
    string UserId,
    string Email,
    string FirstName,
    string LastName) : IRequest<Result<UserDto>>;
