using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Auth.Commands;

/// <summary>Registers a new user account and returns auth tokens.</summary>
public sealed record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<Result<AuthResponse>>;
