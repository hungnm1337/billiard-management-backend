
using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.RewardPoint
{
    public class RewardPointRepository : IRewardPointRepository
    {
        private readonly Prn232ProjectContext _context;

        public RewardPointRepository(Prn232ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Models.RewardPoint rewardPoint)
        {
            await _context.RewardPoints.AddAsync(rewardPoint);
        }

        public void Delete(Models.RewardPoint rewardPoint)
        {
            _context.RewardPoints.Remove(rewardPoint);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.RewardPoints.AnyAsync(rp => rp.RewardPointsId == id);
        }

        public async Task<IEnumerable<Models.RewardPoint>> GetAllAsync()
        {
            return await _context.RewardPoints
                .Include(rp => rp.User)
                .OrderByDescending(rp => rp.Points)
                .ToListAsync();
        }

        public async Task<Models.RewardPoint?> GetByIdAsync(int id)
        {
            return await _context.RewardPoints
                .Include(rp => rp.User)
                .FirstOrDefaultAsync(rp => rp.RewardPointsId == id);
        }

        public async Task<Models.RewardPoint?> GetByUserIdAsync(int userId)
        {
            return await _context.RewardPoints
               .Include(rp => rp.User)
               .FirstOrDefaultAsync(rp => rp.UserId == userId);
        }

        public Task<IEnumerable<Models.RewardPoint>> GetByUserIdsAsync(IEnumerable<int> userIds)
        {
            throw new NotImplementedException();
        }

        public async Task<double> GetUserPointsAsync(int userId)
        {
            var rewardPoint = await _context.RewardPoints
                 .FirstOrDefaultAsync(rp => rp.UserId == userId);

            return rewardPoint?.Points ?? 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update(Models.RewardPoint rewardPoint)
        {
            _context.RewardPoints.Update(rewardPoint);
        }

        public async Task<bool> UserHasRewardPointAsync(int userId)
        {
            return await _context.RewardPoints.AnyAsync(rp => rp.UserId == userId);
        }
    }
}
