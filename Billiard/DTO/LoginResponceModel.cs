namespace Billiard.DTO
{
    public class LoginResponceModel
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
