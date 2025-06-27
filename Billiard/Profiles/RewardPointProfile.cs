// Profiles/RewardPointProfile.cs
using AutoMapper;
using Billiard.Models;
using Billiard.DTO;

namespace Billiard.Profiles
{
    public class RewardPointProfile : Profile
    {
        public RewardPointProfile()
        {
            CreateMap<RewardPoint, RewardPointDto>();
            CreateMap<CreateRewardPointDto, RewardPoint>();
            CreateMap<UpdateRewardPointDto, RewardPoint>();
        }
    }
}
