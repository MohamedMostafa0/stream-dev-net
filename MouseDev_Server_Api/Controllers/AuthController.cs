using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MouseDev_Server_Api.Services.Auth;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace MouseDev_Server_Api.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthenticateService _authService;
        public AuthController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                HttpContext.Session.SetString("JWToken", token);
                return Ok(token);
            }

            return BadRequest("Invalid Request");
        }
        [HttpPost]
        [Route("refresh")]
        public IActionResult RefreshToken()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_authService.GetPrincipalFromExpiredToken(HttpContext.Session.GetString("JWToken"), out string newToken))
            {
                HttpContext.Session.SetString("JWToken", newToken);
                return Ok(newToken);
            }
            else
            {
                return BadRequest("Invalid Request");
            }
        }

        [Authorize]
        [Route("signout")]
        [HttpPost]
        public IActionResult Signout()
        {
            try
            {
                HttpContext.Session.Clear();
                return CreateAuthModel();
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        private JsonResult CreateAuthModel()
        {
            Claim userClaim = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name);
            AuthModel authModel = JsonConvert.DeserializeObject<AuthModel>(userClaim.Value);
            return new JsonResult(authModel);
        }
    }
}