using System.Collections.Generic;
using System.Security.Claims;

namespace ShatteredRealms.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string userId, string email, List<string> roles, List<string> permissions);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
