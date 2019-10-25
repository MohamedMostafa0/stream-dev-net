using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MouseDev_Server_Api.Services.Auth
{
    public class TokenRequest
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
