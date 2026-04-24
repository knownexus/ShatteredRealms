using MediatR;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Users;

public sealed class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, Result<UserDto>>
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public ApproveUserCommandHandler(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

    public async Task<Result<UserDto>> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
    {
        var approveResult = await _userService.ApproveUserAsync(request.TargetUserId, cancellationToken);
        if (approveResult.IsFailure)
        {
            return Result.Failure<UserDto>(approveResult.Error);
        }

        var user = approveResult.Value;

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
