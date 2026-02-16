using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Auth.Commands;

/// <summary>Revokes an active refresh token, invalidating the session.</summary>
public sealed record RevokeTokenCommand(string RefreshToken, string RequestingIpAddress) : IRequest<Result>;
