namespace Billiard.DTO
{
    public class ProfileModel
    {
        public int UserId { get; set; }

        public string Name { get; set; } 

        public int AccountId { get; set; }

        public int RoleId { get; set; }

        public DateOnly Dob { get; set; }

        public string Username { get; set; } 

        public string Password { get; set; } 

        public string Status { get; set; }

        public double? Salary1 { get; set; }

        public int? RewardPointsId { get; set; }

        public double? Points { get; set; }
    }
}
