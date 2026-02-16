using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Queries;

/// <summary>Returns a single user by their ID.</summary>
public sealed record GetUserByIdQuery(string UserId) : IRequest<Result<UserDto>>;
