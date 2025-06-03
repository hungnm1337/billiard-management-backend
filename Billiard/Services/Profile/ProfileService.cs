using Billiard.DTO;
using Billiard.Repositories.Profile;

namespace Billiard.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileService;
        public ProfileService(IProfileRepository profileService) {
        _profileService = profileService;

        }
        public async Task<bool> EditInformationProfile(ProfileModel usermodel)
        {
           return await _profileService.EditInformationProfile(usermodel);
        }

        public async Task<ProfileModel> GetInformationProfile(int userId)
        {
            return await _profileService.GetInformationProfile(userId);
        }
    }
}
