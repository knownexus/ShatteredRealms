using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Queries;

public sealed record GetPendingUsersQuery : IRequest<Result<List<UserDto>>>;
