using Billiard.Models;
namespace Billiard.Repositories.RewardPoint
{
    public interface IRewardPointRepository
    {
        Task<IEnumerable<Models.RewardPoint>> GetAllAsync();
        Task<Models.RewardPoint?> GetByIdAsync(int id);
        Task<Models.RewardPoint?> GetByUserIdAsync(int userId);
        Task<IEnumerable<Models.RewardPoint>> GetByUserIdsAsync(IEnumerable<int> userIds);
        Task AddAsync(Models.RewardPoint rewardPoint);
        void Update(Models.RewardPoint rewardPoint);
        void Delete(Models.RewardPoint rewardPoint);
        Task<bool> ExistsAsync(int id);
        Task<bool> UserHasRewardPointAsync(int userId);
        Task<double> GetUserPointsAsync(int userId);
        Task<bool> SaveChangesAsync();
    }
}
