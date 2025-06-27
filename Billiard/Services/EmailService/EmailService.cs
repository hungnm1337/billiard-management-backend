using Microsoft.Extensions.Options;
using Billiard.DTO;
using System.Net.Mail;
using System.Net;
using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IConfiguration _configuration;
        private readonly Prn232ProjectContext _projectContext;

        public EmailService(IOptions<EmailSettings> emailSettings, IConfiguration configuration, Prn232ProjectContext projectContext)
        {
            _configuration = configuration;
            _projectContext = projectContext;
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> sendPassword(int accountId, string password)
        {
            try
            {
                var account = await _projectContext.Accounts.FindAsync(accountId);
                var user = await _projectContext.Users.FirstOrDefaultAsync(x => x.AccountId == account.AccountId);
                if (account == null)
                {
                    return false;
                }
                string toEmail = account.Username;
                string username = user.Name ?? "Khách hàng";

                if (string.IsNullOrEmpty(toEmail))
                {
                    Console.WriteLine("User email is null or empty");
                    return false;
                }

                // Validate email format
                if (!IsValidEmail(toEmail))
                {
                    Console.WriteLine($"Invalid email format: {toEmail}");
                    return false;
                }

                string fromEmail = _configuration["EmailSettings:Email"];
                string fromPassword = _configuration["EmailSettings:Password"];

                // Debug thông tin
                Console.WriteLine($"Sending email from: {fromEmail} to: {toEmail}");

                var fromAddress = new MailAddress(fromEmail, "Billiard OTP Service");
                var toAddress = new MailAddress(toEmail, username);
                string subject = "Mật khẩu mới - Billiard Club";
                string body = GenerateResetPasswodEmailBody(username, password);

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtp.Timeout = 200000;

                    using (var message = new MailMessage(fromAddress, toAddress))
                    {
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        Console.WriteLine("Attempting to send email...");
                        await smtp.SendMailAsync(message);
                        Console.WriteLine("Email sent successfully");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendOtpEmailAsync(int orderid, string otpCode)
        {
            try
            {
                // Sử dụng Include để load tất cả related data một lần
                OrderTable o = await _projectContext.OrderTables
                    .Include(x => x.User)
                    .ThenInclude(u => u.Account)
                    .FirstOrDefaultAsync(x => x.Id == orderid);

                if (o == null)
                {
                    Console.WriteLine($"OrderTable not found for ID: {orderid}");
                    return false;
                }

                if (o.User == null)
                {
                    Console.WriteLine($"User not found for OrderTable ID: {orderid}");
                    return false;
                }

                if (o.User.Account == null)
                {
                    Console.WriteLine($"Account not found for User ID: {o.User.UserId}");
                    return false;
                }

                // Lấy thông tin email và validate
                string toEmail = o.User.Account.Username;
                string username = o.User.Name ?? "Khách hàng";

                if (string.IsNullOrEmpty(toEmail))
                {
                    Console.WriteLine("User email is null or empty");
                    return false;
                }

                // Validate email format
                if (!IsValidEmail(toEmail))
                {
                    Console.WriteLine($"Invalid email format: {toEmail}");
                    return false;
                }

                string fromEmail = _configuration["EmailSettings:Email"];
                string fromPassword = _configuration["EmailSettings:Password"];

                // Debug thông tin
                Console.WriteLine($"Sending email from: {fromEmail} to: {toEmail}");

                var fromAddress = new MailAddress(fromEmail, "Billiard OTP Service");
                var toAddress = new MailAddress(toEmail, username);
                string subject = "Mã OTP xác nhận đặt bàn - Billiard Club";
                string body = GenerateOtpEmailBody(username, otpCode);

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false; // Quan trọng: phải set false
                    smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtp.Timeout = 200000;

                    using (var message = new MailMessage(fromAddress, toAddress))
                    {
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        Console.WriteLine("Attempting to send email...");
                        await smtp.SendMailAsync(message);
                        Console.WriteLine("Email sent successfully");
                    }
                }

                return true;
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Error: {smtpEx.Message}");
                Console.WriteLine($"Status Code: {smtpEx.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        // Thêm method validation email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }

        private string GenerateOtpEmailBody(string customerName, string otpCode)
        {
            return $@"
            <html>
            <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
                    <h2 style='color: #2c3e50; text-align: center; margin-bottom: 30px;'>🎱 Xác nhận đặt bàn Billiard</h2>
                    
                    <p>Xin chào <strong>{customerName}</strong>,</p>
                    
                    <p>Cảm ơn bạn đã đặt bàn tại quán billiard của chúng tôi. Để hoàn tất việc đặt bàn, vui lòng sử dụng mã OTP dưới đây:</p>
                    
                    <div style='text-align: center; margin: 30px 0;'>
                        <div style='background-color: #3498db; color: white; font-size: 32px; font-weight: bold; padding: 20px; border-radius: 8px; letter-spacing: 5px;'>
                            {otpCode}
                        </div>
                        <p style='color: #7f8c8d; font-size: 14px; margin-top: 10px;'>Mã OTP có hiệu lực trong 5 phút</p>
                    </div>
                    
                    <p style='color: #7f8c8d; font-size: 14px; text-align: center; margin-top: 30px;'>
                        Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.
                    </p>
                    
                    <hr style='border: none; border-top: 1px solid #ecf0f1; margin: 30px 0;'>
                    <p style='text-align: center; color: #95a5a6; font-size: 12px;'>
                        © 2025 Billiard Management System. All rights reserved.
                    </p>
                </div>
            </body>
            </html>";
        }

        private string GenerateResetPasswodEmailBody(string customerName, string password)
        {
            return $@"
            <html>
            <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
                    <h2 style='color: #2c3e50; text-align: center; margin-bottom: 30px;'>Mật khẩu mới</h2>
                    
                    <p>Xin chào <strong>{customerName}</strong>,</p>
                    
                    <p>Cảm ơn bạn đã đặt lại mật khẩu. Đây là mật khẩu mới của bạn:</p>
                    
                    <div style='text-align: center; margin: 30px 0;'>
                        <div style='background-color: #3498db; color: white; font-size: 32px; font-weight: bold; padding: 20px; border-radius: 8px; letter-spacing: 5px;'>
                            {password}
                        </div>
                    </div>
                    
                    <p style='color: #7f8c8d; font-size: 14px; text-align: center; margin-top: 30px;'>
                        Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.
                    </p>
                    
                    <hr style='border: none; border-top: 1px solid #ecf0f1; margin: 30px 0;'>
                    <p style='text-align: center; color: #95a5a6; font-size: 12px;'>
                        © 2025 Billiard Management System. All rights reserved.
                    </p>
                </div>
            </body>
            </html>";
        }
    }
}