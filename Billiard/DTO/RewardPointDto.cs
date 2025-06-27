namespace Billiard.DTO
{
    public class RewardPointDto
    {
        public int RewardPointsId { get; set; }
        public int UserId { get; set; }
        public double Points { get; set; }
    }

    public class CreateRewardPointDto
    {
        public int UserId { get; set; }
        public double Points { get; set; }
    }

    public class UpdateRewardPointDto
    {
        public int UserId { get; set; }
        public double Points { get; set; }
    }

    public class AddPointsDto
    {
        public int UserId { get; set; }
        public double PointsToAdd { get; set; }
        public string? Description { get; set; }
    }

    public class DeductPointsDto
    {
        public int UserId { get; set; }
        public double PointsToDeduct { get; set; }
        public string? Description { get; set; }
    }
}
