// Services/RewardPoint/RewardPointService.cs
using AutoMapper;
using Billiard.DTO;
using Billiard.Models;
using Billiard.Repositories.RewardPoint;

namespace Billiard.Services.RewardPoints // ✅ Giữ nguyên namespace này vì Program.cs đã import
{
    public class RewardPointService : IRewardPointService
    {
        private readonly IRewardPointRepository _rewardPointRepository;
        private readonly IMapper _mapper;

        // ✅ Thêm IMapper vào constructor
        public RewardPointService(IRewardPointRepository rewardPointRepository, IMapper mapper)
        {
            _rewardPointRepository = rewardPointRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RewardPointDto>> GetAllRewardPointsAsync()
        {
            try
            {
                var rewardPoints = await _rewardPointRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RewardPointDto>>(rewardPoints);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi lấy danh sách reward points", ex);
            }
        }

        public async Task<RewardPointDto?> GetRewardPointByIdAsync(int id)
        {
            try
            {
                var rewardPoint = await _rewardPointRepository.GetByIdAsync(id);
                return rewardPoint != null ? _mapper.Map<RewardPointDto>(rewardPoint) : null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lấy reward point với ID {id}", ex);
            }
        }

        public async Task<RewardPointDto?> GetRewardPointByUserIdAsync(int userId)
        {
            try
            {
                var rewardPoint = await _rewardPointRepository.GetByUserIdAsync(userId);
                return rewardPoint != null ? _mapper.Map<RewardPointDto>(rewardPoint) : null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lấy reward point của user {userId}", ex);
            }
        }

        public async Task<RewardPointDto> CreateRewardPointAsync(CreateRewardPointDto createDto)
        {
            try
            {
                // Validate input
                if (createDto == null)
                    throw new ArgumentNullException(nameof(createDto));

                if (createDto.UserId <= 0)
                    throw new ArgumentException("UserId phải lớn hơn 0");

                if (createDto.Points < 0)
                    throw new ArgumentException("Points không thể âm");

                // Kiểm tra user đã có reward point chưa
                if (await _rewardPointRepository.UserHasRewardPointAsync(createDto.UserId))
                {
                    throw new InvalidOperationException($"User với ID {createDto.UserId} đã có reward point");
                }

                // ✅ Sử dụng AutoMapper
                var rewardPoint = _mapper.Map<RewardPoint>(createDto);

                await _rewardPointRepository.AddAsync(rewardPoint);
                await _rewardPointRepository.SaveChangesAsync();

                return _mapper.Map<RewardPointDto>(rewardPoint);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi tạo reward point", ex);
            }
        }

        public async Task<RewardPointDto?> UpdateRewardPointAsync(int id, UpdateRewardPointDto updateDto)
        {
            try
            {
                // Validate input
                if (updateDto == null)
                    throw new ArgumentNullException(nameof(updateDto));

                if (id <= 0)
                    throw new ArgumentException("ID phải lớn hơn 0");

                if (updateDto.UserId <= 0)
                    throw new ArgumentException("UserId phải lớn hơn 0");

                if (updateDto.Points < 0)
                    throw new ArgumentException("Points không thể âm");

                var existingRewardPoint = await _rewardPointRepository.GetByIdAsync(id);
                if (existingRewardPoint == null)
                {
                    return null;
                }

                // Kiểm tra nếu thay đổi UserId, user mới chưa có reward point
                if (existingRewardPoint.UserId != updateDto.UserId)
                {
                    if (await _rewardPointRepository.UserHasRewardPointAsync(updateDto.UserId))
                    {
                        throw new InvalidOperationException($"User với ID {updateDto.UserId} đã có reward point");
                    }
                }

                // ✅ Sử dụng AutoMapper
                _mapper.Map(updateDto, existingRewardPoint);

                _rewardPointRepository.Update(existingRewardPoint);
                await _rewardPointRepository.SaveChangesAsync();

                return _mapper.Map<RewardPointDto>(existingRewardPoint);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi cập nhật reward point với ID {id}", ex);
            }
        }

        public async Task<bool> DeleteRewardPointAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID phải lớn hơn 0");

                var rewardPoint = await _rewardPointRepository.GetByIdAsync(id);
                if (rewardPoint == null)
                {
                    return false;
                }

                _rewardPointRepository.Delete(rewardPoint);
                return await _rewardPointRepository.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi xóa reward point với ID {id}", ex);
            }
        }

        public async Task<RewardPointDto?> AddPointsAsync(AddPointsDto addPointsDto)
        {
            try
            {
                // Validate input
                if (addPointsDto == null)
                    throw new ArgumentNullException(nameof(addPointsDto));

                if (addPointsDto.UserId <= 0)
                    throw new ArgumentException("UserId phải lớn hơn 0");

                if (addPointsDto.PointsToAdd <= 0)
                    throw new ArgumentException("Points to add phải lớn hơn 0");

                var rewardPoint = await _rewardPointRepository.GetByUserIdAsync(addPointsDto.UserId);

                if (rewardPoint == null)
                {
                    // Tạo mới nếu user chưa có reward point
                    rewardPoint = new RewardPoint
                    {
                        UserId = addPointsDto.UserId,
                        Points = addPointsDto.PointsToAdd
                    };
                    await _rewardPointRepository.AddAsync(rewardPoint);
                }
                else
                {
                    // Cộng thêm points
                    rewardPoint.Points += addPointsDto.PointsToAdd;
                    _rewardPointRepository.Update(rewardPoint);
                }

                await _rewardPointRepository.SaveChangesAsync();
                return _mapper.Map<RewardPointDto>(rewardPoint);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi cộng points cho user {addPointsDto?.UserId}", ex);
            }
        }

        public async Task<RewardPointDto?> DeductPointsAsync(DeductPointsDto deductPointsDto)
        {
            try
            {
                // Validate input
                if (deductPointsDto == null)
                    throw new ArgumentNullException(nameof(deductPointsDto));

                if (deductPointsDto.UserId <= 0)
                    throw new ArgumentException("UserId phải lớn hơn 0");

                if (deductPointsDto.PointsToDeduct <= 0)
                    throw new ArgumentException("Points to deduct phải lớn hơn 0");

                var rewardPoint = await _rewardPointRepository.GetByUserIdAsync(deductPointsDto.UserId);

                if (rewardPoint == null)
                {
                    throw new InvalidOperationException($"User với ID {deductPointsDto.UserId} không có reward point");
                }

                if (rewardPoint.Points < deductPointsDto.PointsToDeduct)
                {
                    throw new InvalidOperationException($"Không đủ points để trừ. Hiện có: {rewardPoint.Points}, cần trừ: {deductPointsDto.PointsToDeduct}");
                }

                rewardPoint.Points -= deductPointsDto.PointsToDeduct;
                _rewardPointRepository.Update(rewardPoint);
                await _rewardPointRepository.SaveChangesAsync();

                return _mapper.Map<RewardPointDto>(rewardPoint);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi trừ points cho user {deductPointsDto?.UserId}", ex);
            }
        }

        public async Task<double> GetUserPointsAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("UserId phải lớn hơn 0");

                return await _rewardPointRepository.GetUserPointsAsync(userId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lấy points của user {userId}", ex);
            }
        }

        public async Task<bool> RewardPointExistsAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID phải lớn hơn 0");

                return await _rewardPointRepository.ExistsAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi kiểm tra tồn tại reward point với ID {id}", ex);
            }
        }
    }
}
