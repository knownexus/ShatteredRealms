using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Users.Commands;

public sealed record ApproveUserCommand(string TargetUserId) : IRequest<Result<UserDto>>;
