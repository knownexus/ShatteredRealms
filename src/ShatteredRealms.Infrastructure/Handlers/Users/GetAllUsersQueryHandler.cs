using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.Features.Users.Queries;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Application.Mappers;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Users;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserService _userService;

    public GetAllUsersQueryHandler(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
    }

    public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        if (users.IsFailure)
        {
            return Result.Failure<List<UserDto>>(users.Error);
        }

        var dtos = users.Value.Select(u => UserMapper.ToDto(
            u,
            roles: u.UserRoles.Select(ur => ur.Role.Name!).ToList(),
            permissions: new List<string>())).ToList();

        return Result.Success(dtos);
    }
}
