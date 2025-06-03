using Billiard.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billiard.Services.Profile;
using Billiard.DTO;

namespace Billiard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID");
                }

                var profile = await _profileService.GetInformationProfile(userId);

                if (profile == null)
                {
                    return NotFound("Profile not found");
                }

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel profileModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (profileModel == null)
                {
                    return BadRequest("Profile data is required");
                }

                var result = await _profileService.EditInformationProfile(profileModel);

                if (result)
                {
                    return Ok(new { message = "Profile updated successfully" });
                }
                else
                {
                    return BadRequest("Failed to update profile");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

      
    }
}

