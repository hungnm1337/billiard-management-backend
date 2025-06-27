using Billiard.Services.Account;
using Billiard.Services.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Account>>> GetAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy danh sách accounts", error = ex.Message });
            }
        }

        /// <summary>
        /// Thay đổi trạng thái account
        /// </summary>
        /// <param name="accountId">ID của account cần thay đổi trạng thái</param>
        /// <returns>True nếu thành công, False nếu thất bại</returns>
        [HttpPut("{accountId}/status")]
        public async Task<ActionResult<bool>> ChangeStatusAccount(int accountId)
        {
            try
            {
                if (accountId <= 0)
                {
                    return BadRequest(new { message = "Account ID không hợp lệ" });
                }

                var result = await _accountService.changeStatusAccount(accountId);

                if (result)
                {
                    return Ok(new { success = true, message = "Thay đổi trạng thái account thành công" });
                }
                else
                {
                    return NotFound(new { success = false, message = "Không tìm thấy account hoặc không thể thay đổi trạng thái" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi thay đổi trạng thái account", error = ex.Message });
            }
        }

        /// <summary>
        /// Reset password cho account
        /// </summary>
        /// <param name="accountId">ID của account cần reset password</param>
        /// <returns>Password mới được tạo</returns>
        [HttpPost("{accountId}/reset-password")]
        public async Task<ActionResult<string>> ResetPassword(int accountId)
        {
            try
            {
                if (accountId <= 0)
                {
                    return BadRequest(new { message = "Account ID không hợp lệ" });
                }

                var newPassword = await _accountService.resetPassword(accountId);

                if (!string.IsNullOrEmpty(newPassword))
                {
                    bool resultSendEmail = await  _emailService.sendPassword(accountId, newPassword);

                    if (resultSendEmail)
                    {
                        return Ok(new
                        {
                            success = true,
                            message = "Reset password thành công",
                            newPassword = newPassword
                        });
                    }
                    else
                    {
                        return NotFound(new { success = false, message = "Gửi email bị lỗi" });

                    }
                }
                else
                {
                    return NotFound(new { success = false, message = "Không tìm thấy account hoặc không thể reset password" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi reset password", error = ex.Message });
            }
        }

    }
}
