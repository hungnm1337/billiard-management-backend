using Billiard.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/byRole/{id}")]
        public async Task<ActionResult<IEnumerable<Models.User>>> GetUserList(int roleId)
        {
            var users = await _userService.getUserList(roleId);
            return Ok(users);
        }

        [HttpGet("/byId/{id}")]
        public async Task<ActionResult<Models.User>> GetUserById(int id)
        {
            var userGetById = await _userService.getuserById(id);
            return Ok(userGetById);
        }

        [HttpGet("/byName")]
        public async Task<ActionResult<Models.User>> GetUserByName(string userName)
        {
            var userGetByName = await _userService.getuserByName(userName);
            return Ok(userGetByName);
        }
    }
}
