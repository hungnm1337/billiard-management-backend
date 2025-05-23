using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestJwtController : ControllerBase
    {
        // Endpoint mở, ai cũng truy cập được
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new { message = "Đây là endpoint public, không cần JWT." });
        }

        // Endpoint bảo vệ, chỉ truy cập được khi có JWT hợp lệ
        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            // Lấy thông tin user từ JWT (nếu cần)
            var username = User.Identity?.Name;
            return Ok(new
            {
                message = "Bạn đã truy cập endpoint bảo vệ thành công!",
                username = username
            });
        }
    }
}
