using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Commands;

/// <summary>Deactivates and deletes a user account.</summary>
public sealed record DeleteUserCommand(string UserId) : IRequest<Result>;
