using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MouseDev_Server_Api.Services.Auth;

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
        [Authorize]
        [Route("signout")]
        [HttpPost]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Ok();
        }
    }
}