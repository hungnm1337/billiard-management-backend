// Controllers/RewardPointsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Billiard.Services;
using Billiard.DTOs;
using Billiard.DTO;
using Billiard.Services.RewardPoints;

namespace Billiard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardPointsController : ControllerBase
    {
        private readonly IRewardPointService _rewardPointService;

        public RewardPointsController(IRewardPointService rewardPointService)
        {
            _rewardPointService = rewardPointService;
        }

        /// <summary>
        /// Lấy danh sách tất cả reward points
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RewardPointDto>>> GetAllRewardPoints()
        {
            try
            {
                var rewardPoints = await _rewardPointService.GetAllRewardPointsAsync();
                return Ok(rewardPoints);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy reward point theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RewardPointDto>> GetRewardPoint(int id)
        {
            try
            {
                var rewardPoint = await _rewardPointService.GetRewardPointByIdAsync(id);
                if (rewardPoint == null)
                {
                    return NotFound(new { message = $"Không tìm thấy reward point với ID {id}" });
                }

                return Ok(rewardPoint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy reward point theo User ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<RewardPointDto>> GetRewardPointByUserId(int userId)
        {
            try
            {
                var rewardPoint = await _rewardPointService.GetRewardPointByUserIdAsync(userId);
                if (rewardPoint == null)
                {
                    return NotFound(new { message = $"User với ID {userId} chưa có reward point" });
                }

                return Ok(rewardPoint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy số points của user
        /// </summary>
        [HttpGet("user/{userId}/points")]
        public async Task<ActionResult<double>> GetUserPoints(int userId)
        {
            try
            {
                var points = await _rewardPointService.GetUserPointsAsync(userId);
                return Ok(new { userId, points });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Tạo reward point mới
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RewardPointDto>> CreateRewardPoint(CreateRewardPointDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rewardPoint = await _rewardPointService.CreateRewardPointAsync(createDto);
                return CreatedAtAction(nameof(GetRewardPoint), new { id = rewardPoint.RewardPointsId }, rewardPoint);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật reward point
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<RewardPointDto>> UpdateRewardPoint(int id, UpdateRewardPointDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rewardPoint = await _rewardPointService.UpdateRewardPointAsync(id, updateDto);
                if (rewardPoint == null)
                {
                    return NotFound(new { message = $"Không tìm thấy reward point với ID {id}" });
                }

                return Ok(rewardPoint);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Xóa reward point
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRewardPoint(int id)
        {
            try
            {
                var result = await _rewardPointService.DeleteRewardPointAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Không tìm thấy reward point với ID {id}" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Cộng points cho user
        /// </summary>
        [HttpPost("add-points")]
        [Authorize]
        public async Task<ActionResult<RewardPointDto>> AddPoints(AddPointsDto addPointsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rewardPoint = await _rewardPointService.AddPointsAsync(addPointsDto);
                return Ok(rewardPoint);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Trừ points của user
        /// </summary>
        [HttpPost("deduct-points")]
        [Authorize]
        public async Task<ActionResult<RewardPointDto>> DeductPoints(DeductPointsDto deductPointsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rewardPoint = await _rewardPointService.DeductPointsAsync(deductPointsDto);
                return Ok(rewardPoint);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        /// <summary>
        /// Kiểm tra reward point có tồn tại không
        /// </summary>
        [HttpHead("{id}")]
        public async Task<IActionResult> CheckRewardPointExists(int id)
        {
            try
            {
                var exists = await _rewardPointService.RewardPointExistsAsync(id);
                return exists ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }
    }
}
