using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MouseDev_Server_Api.Helpers;
using MouseDev_Server_Api.Services.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MouseDev_Server_Api.Services.Auth
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            try
            {
                _userManagementService = service;
                _tokenManagement = tokenManagement.Value;
            }
            catch { }
        }
        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            token = string.Empty;
            try
            {
                if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;

                AuthModel authModel = new AuthModel()
                {
                    Name = request.Username,
                    Email = request.Username + "@yahoo.com"
                };
                string userJson = JsonConvert.SerializeObject(authModel);
                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Name, userJson),
                };
                return GenerateToken(claims, _tokenManagement.AccessExpiration, out token);
            }
            catch
            {
                return false;
            }

        }
        public bool GetPrincipalFromExpiredToken(string oldToken, out string newToken)
        {
            newToken = string.Empty;
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal principal = tokenHandler.ValidateToken(oldToken, tokenValidationParameters, out SecurityToken securityToken);
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return false; // mohamed throw exception invalid token

                return GenerateToken(principal.Claims, _tokenManagement.RefreshExpiration, out newToken);
            }
            catch
            {
                return false;
            }
        }
        public bool GenerateToken(IEnumerable<Claim> claims, int time, out string token)
        {
            token = string.Empty;
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken jwtToken = new JwtSecurityToken(
                    _tokenManagement.Issuer,
                    _tokenManagement.Audience,
                    claims,
                    expires: DateTime.Now.AddSeconds(time),
                    signingCredentials: credentials
                );

                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
