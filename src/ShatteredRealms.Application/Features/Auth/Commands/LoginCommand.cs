using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Auth.Commands;

/// <summary>Authenticates a user and returns auth tokens.</summary>
public sealed record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;
