using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MouseDev_Server_Api.Database.BL;
using MouseDev_Server_Api.Helpers;
using MouseDev_Server_Api.Services.Auth;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MouseDev_Server_Api.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthenticateService _authService;
        private readonly IClientBL _clientRepository;
        public AuthController(IAuthenticateService authService , IClientBL clientRepository)
        {
            _authService = authService;
            _clientRepository = clientRepository;
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                HttpContext.Session.SetString("JWToken", token);
                return await ResultHandler.Success();
            }

            return BadRequest("Invalid Request");
        }
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_authService.GetPrincipalFromExpiredToken(HttpContext.Session.GetString("JWToken"), out string newToken))
            {
                HttpContext.Session.SetString("JWToken", newToken);
                return await ResultHandler.Success();
            }
            else
            {
                return BadRequest("Invalid Request");
            }
        }

        [Authorize]
        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            try
            {
                HttpContext.Session.Clear();
                return await CreateAuthModel();
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        private async Task<JsonResult> CreateAuthModel()
        {
            Claim userClaim = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name);
            AuthModel authModel = JsonConvert.DeserializeObject<AuthModel>(userClaim.Value);
            return await ResultHandler.Success(authModel);
        }
    }
}