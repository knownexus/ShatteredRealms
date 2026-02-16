using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Auth.Commands;

/// <summary>Rotates a refresh token and issues new auth tokens.</summary>
public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthResponse>>;
