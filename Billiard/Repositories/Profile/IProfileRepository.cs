using Billiard.DTO;

namespace Billiard.Repositories.Profile
{
    public interface IProfileRepository
    {
        Task<ProfileModel> GetInformationProfile(int userId);

        Task<bool> EditInformationProfile(ProfileModel usermodel);
    }
}
