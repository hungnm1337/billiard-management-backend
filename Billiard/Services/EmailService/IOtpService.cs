namespace Billiard.Services.EmailService
{
    public interface IOtpService
    {
        public string GenerateOtp();

        public void SaveOtp(string otpCode, int orderTableId);

        public bool VerifyOtp(int orderID, string otpCode);


    }
}
