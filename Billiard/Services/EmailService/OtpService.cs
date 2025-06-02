using Billiard.DTO;
using Billiard.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Billiard.Services.EmailService
{
    public class OtpService : IOtpService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Prn232ProjectContext _projectContext;
        public OtpService(IHttpContextAccessor httpContextAccessor, Prn232ProjectContext projectContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectContext = projectContext;
        }

        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void SaveOtp(string otpCode, int orderTableId)
        {
            var se = _httpContextAccessor.HttpContext.Session;
            if (se == null)
            {
                Console.WriteLine("❌ Session is null!");
                throw new InvalidOperationException("Session is not available");
            }
            var key = $"otp_{orderTableId}";

            var otpData = new
            {
                Code = otpCode,
                OrderTableId = orderTableId,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5) 
            };

            se.SetString(key, JsonSerializer.Serialize(otpData));
            Console.WriteLine($"Saved OTP: {otpCode} for order {orderTableId}, expires at {otpData.ExpiresAt}");
        }


        // Sửa lại VerifyOtp method
        public bool VerifyOtp(int orderID, string otpCode)
        {
     
            var session = _httpContextAccessor.HttpContext.Session;
            var key = $"otp_{orderID}";
            var otpDataJson = session.GetString(key);

            if (string.IsNullOrEmpty(otpDataJson))
                return false;

            try
            {
                var otpData = JsonSerializer.Deserialize<JsonElement>(otpDataJson);
                var storedCode = otpData.GetProperty("Code").GetString();

                if (storedCode.Equals(otpCode))
                {
                    session.Remove(key);
                    return true;
                }
            }
            catch
            {
                session.Remove(key);
            }

            return false;
        }

    }
}

