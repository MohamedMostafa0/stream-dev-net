namespace MouseDev_Server_Api.Services.Auth
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
    }
}
