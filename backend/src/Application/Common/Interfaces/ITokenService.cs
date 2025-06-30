using System.Security.Claims;

namespace UniverSys.Application.Common.Interfaces;
public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    bool ValidateCurrentToken(string token);
}
