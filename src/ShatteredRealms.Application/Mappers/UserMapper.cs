using System.Collections.Generic;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;

namespace ShatteredRealms.Application.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(User user, List<string> roles, List<string> permissions) => new()
    {
        Id = user.Id, Email = user.Email!, FirstName = user.FirstName, LastName = user.LastName, Roles = roles
      , Permissions = permissions
    };

    public static UserDto ToDto(User user) => ToDto(user, new List<string>(), new List<string>());
}