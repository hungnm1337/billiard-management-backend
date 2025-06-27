namespace Billiard.Services.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendOtpEmailAsync(int orderid, string otpCode);
        Task<bool> sendPassword(int accountId, string password);
    }
}
