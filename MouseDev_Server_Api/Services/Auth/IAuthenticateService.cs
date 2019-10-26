using System.Collections.Generic;
using System.Security.Claims;

namespace MouseDev_Server_Api.Services.Auth
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
        bool GetPrincipalFromExpiredToken(string oldToken , out string newToken);
        bool GenerateToken(IEnumerable<Claim> claims , int time , out string token);
    }
}
