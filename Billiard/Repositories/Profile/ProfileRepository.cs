using Billiard.DTO;
using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Prn232ProjectContext _projectContext;
        public ProfileRepository(Prn232ProjectContext context) {
        _projectContext = context;
        }

        public Task<bool> EditInformationProfile(ProfileModel usermodel)
        {
            throw new NotImplementedException();
        }

        public async Task<ProfileModel> GetInformationProfile(int userId)
        {
            try
            {
                // Lấy user với tất cả thông tin liên quan trong một query
                var user = await _projectContext.Users
                    .Include(x => x.Account)
                    .Include(x => x.RewardPoints)
                    .Include(x => x.Salaries) // Nếu có navigation property
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                if (user == null)
                {
                    return null; // Hoặc throw exception tùy theo business logic
                }

                // Lấy salary riêng nếu không có navigation property
                var salary = await _projectContext.Salaries
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                // Lấy reward points riêng nếu cần (hoặc dùng từ user.RewardPoints)
                var rewardPoint = user.RewardPoints?.FirstOrDefault() ??
                                 await _projectContext.RewardPoints
                                     .FirstOrDefaultAsync(x => x.UserId == userId);

                var result = new ProfileModel()
                {
                    UserId = userId,
                    Name = user.Name,
                    AccountId = user.AccountId,
                    RoleId = user.RoleId,
                    Dob = user.Dob,
                    Username = user.Account?.Username,
                    Password = user.Account?.Password, // Cân nhắc bảo mật
                    Status = user.Account?.Status,
                    Salary1 = salary?.Salary1 ?? 0,
                    RewardPointsId = rewardPoint?.RewardPointsId ?? 0,
                    Points = rewardPoint?.Points ?? 0
                };

                return result;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Error getting profile for user {userId}: {ex.Message}", ex);
            }
        }

    }
}
