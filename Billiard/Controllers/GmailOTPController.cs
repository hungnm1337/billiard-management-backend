using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billiard.Services.EmailService;
using Billiard.Services.Table;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailOTPController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IOtpService _otpService;
        private readonly ITableService _tb;

        public GmailOTPController(IEmailService emailService, IOtpService otpService,ITableService tb)
        {
            _emailService = emailService;
            _otpService = otpService;
            _tb = tb;
        }

        /// <summary>
        /// Gửi mã OTP qua email cho việc xác nhận đặt bàn
        /// </summary>
        /// <param name="orderTableId">ID của đơn đặt bàn</param>
        /// <returns>Trạng thái gửi email</returns>
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] int orderTableId)
        {
            try
            {
                // Tạo mã OTP
                string otpCode = _otpService.GenerateOtp();

                // Lưu OTP vào session
                _otpService.SaveOtp(otpCode, orderTableId);

                // Gửi email chứa OTP
                await _emailService.SendOtpEmailAsync(orderTableId, otpCode);

                return Ok(new
                {
                    success = true,
                    message = "Mã OTP đã được gửi đến email của bạn. Vui lòng kiểm tra hộp thư.",
                    otp = otpCode
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi gửi OTP: " + ex.Message,
                    otp = ""

                });
            }
        }

        /// <summary>
        /// Xác minh mã OTP
        /// </summary>
        /// <param name="request">Thông tin xác minh OTP</param>
        /// <returns>Kết quả xác minh</returns>

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            try
            {
                // Debug session
                Console.WriteLine($"Session ID: {HttpContext.Session.Id}");
                Console.WriteLine($"Cookies: {Request.Headers.Cookie}");

                var key = $"otp_{request.OrderTableId}";
                var otpData = HttpContext.Session.GetString(key);
                Console.WriteLine($"OTP data from session: {otpData}");

                if (request == null)
                    return BadRequest(new { success = false, message = "Request body is null" });

                if (string.IsNullOrEmpty(request.OtpCode))
                    return BadRequest(new { success = false, message = "Vui lòng nhập mã OTP" });

                bool isValid = _otpService.VerifyOtp(request.OrderTableId, request.OtpCode);

                if (isValid)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Xác minh OTP thành công. Đặt bàn đã được xác nhận."
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Mã OTP không hợp lệ hoặc đã hết hạn"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest(new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi xác minh OTP: " + ex.Message
                });
            }
        }


        /// <summary>
        /// Gửi lại mã OTP mới
        /// </summary>
        /// <param name="orderTableId">ID của đơn đặt bàn</param>
        /// <returns>Trạng thái gửi lại OTP</returns>
        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] int orderTableId)
        {
            try
            {
                // Tạo mã OTP mới
                string newOtpCode = _otpService.GenerateOtp();

                // Lưu OTP mới vào session (sẽ ghi đè OTP cũ)
                _otpService.SaveOtp(newOtpCode, orderTableId);

                // Gửi email chứa OTP mới
                await _emailService.SendOtpEmailAsync(orderTableId, newOtpCode);

                return Ok(new
                {
                    success = true,
                    message = "Mã OTP mới đã được gửi đến email của bạn."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi gửi lại OTP: " + ex.Message
                });
            }
        }
    }

    /// <summary>
    /// Model cho request xác minh OTP
    /// </summary>
    public class VerifyOtpRequest
    {
        public int OrderTableId { get; set; }
        public string OtpCode { get; set; }
    }
}
