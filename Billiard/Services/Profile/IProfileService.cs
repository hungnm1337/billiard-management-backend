using Billiard.DTO;

namespace Billiard.Services.Profile
{
    public interface IProfileService
    {
        Task<ProfileModel> GetInformationProfile(int userId);

        Task<bool> EditInformationProfile(ProfileModel usermodel);

    }
}
