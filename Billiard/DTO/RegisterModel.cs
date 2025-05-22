namespace Billiard.DTO
{
    public class RegisterModel
    {
        public string name {  get; set; }

        public string username { get; set; }
        public string password { get; set; }

        public DateOnly dob { get; set; }

        public int roleid { get; set; } = 1;

        public string status { get; set; } = "ACTIVE";

    }
}
