using Billiard.DTO;

namespace Billiard.Services.Auth
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, string Message)> LoginAsync(LoginModel model);
    }
}
