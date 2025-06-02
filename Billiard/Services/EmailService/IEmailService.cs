namespace Billiard.Services.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendOtpEmailAsync(int orderid, string otpCode);
    }
}
