using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Users.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Users;

public sealed class GetPendingUsersQueryHandler : IRequestHandler<GetPendingUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserService _userService;

    public GetPendingUsersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<List<UserDto>>> Handle(GetPendingUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetPendingUsersAsync(cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<List<UserDto>>(result.Error);
        }

        var dtos = result.Value
            .Select(u => UserMapper.ToDto(
                u,
                roles: u.UserRoles.Select(ur => ur.Role.Name!).ToList(),
                permissions: new List<string>()))
            .ToList();

        return Result.Success(dtos);
    }
}
