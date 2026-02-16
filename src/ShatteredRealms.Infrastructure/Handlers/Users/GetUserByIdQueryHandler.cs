using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Users.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Users;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public GetUserByIdQueryHandler(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (userResult.IsFailure)
        {
            return Result.Failure<UserDto>(userResult.Error);
        }

        var rolesResult = await _userService.GetUserRolesAsync(request.UserId, cancellationToken);
        if (rolesResult.IsFailure)
        {
            return Result.Failure<UserDto>(rolesResult.Error);
        }

        var permissionsResult = await _permissionService.GetUserPermissionsAsync(request.UserId, cancellationToken);
        if (permissionsResult.IsFailure)
        {
            return Result.Failure<UserDto>(permissionsResult.Error);
        }

        return Result.Success(UserMapper.ToDto(userResult.Value, rolesResult.Value, permissionsResult.Value));
    }
}
