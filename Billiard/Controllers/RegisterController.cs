using Billiard.DTO;
using Billiard.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public RegisterController(AccountService acc)
        {
            _accountService = acc;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await _accountService.Register(model);
                return Ok(model);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }

    }
}
