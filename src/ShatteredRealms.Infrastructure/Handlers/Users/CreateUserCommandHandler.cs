using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Users;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public CreateUserCommandHandler(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateUserRequest
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            RoleIds = request.RoleIds,
        };

        var userResult = await _userService.CreateUserAsync(createRequest, cancellationToken);
        if (userResult.IsFailure)
        {
            return Result.Failure<UserDto>(userResult.Error);
        }

        var user = userResult.Value;
        var rolesResult = await _userService.GetUserRolesAsync(user.Id, cancellationToken);
        if (rolesResult.IsFailure)
        {
            return Result.Failure<UserDto>(rolesResult.Error);
        }

        var permissionsResult = await _permissionService.GetUserPermissionsAsync(user.Id, cancellationToken);
        if (permissionsResult.IsFailure)
        {
            return Result.Failure<UserDto>(permissionsResult.Error);
        }

        return Result.Success(UserMapper.ToDto(user, rolesResult.Value, permissionsResult.Value));
    }
}
