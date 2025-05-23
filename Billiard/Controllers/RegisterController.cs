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
                bool result = await _accountService.Register(model);
                if (result)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest("Username already exsit");
                }
                }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }

    }
}
