using Billiard.DTO;
using Billiard.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
                return BadRequest("Dữ liệu không hợp lệ!");

            var (success, token, message) = await _authService.LoginAsync(model);
            if (!success)
                return Unauthorized(message);

            return Ok(new
            {
                token

            });
        }
    }
}
