using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Queries;

/// <summary>Returns all users as DTOs.</summary>
public sealed record GetAllUsersQuery : IRequest<Result<List<UserDto>>>;
