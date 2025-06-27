using Billiard.DTO;

namespace Billiard.Services.RewardPoints
{
    public interface IRewardPointService
    {
        Task<IEnumerable<RewardPointDto>> GetAllRewardPointsAsync();
        Task<RewardPointDto?> GetRewardPointByIdAsync(int id);
        Task<RewardPointDto?> GetRewardPointByUserIdAsync(int userId);
        Task<RewardPointDto> CreateRewardPointAsync(CreateRewardPointDto createDto);
        Task<RewardPointDto?> UpdateRewardPointAsync(int id, UpdateRewardPointDto updateDto);
        Task<bool> DeleteRewardPointAsync(int id);
        Task<RewardPointDto?> AddPointsAsync(AddPointsDto addPointsDto);
        Task<RewardPointDto?> DeductPointsAsync(DeductPointsDto deductPointsDto);
        Task<double> GetUserPointsAsync(int userId);
        Task<bool> RewardPointExistsAsync(int id);
    }
}
