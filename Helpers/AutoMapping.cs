using AutoMapper;
using HaloBiz.DTOs.ReceivingDTO;
using HaloBiz.DTOs.TransferDTOs;
using HaloBiz.Model;

namespace HaloBiz.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<State, StateTransferDTO>();    
            CreateMap<LGA, LGATransferDTO>();
            CreateMap<UserProfileReceivingDTO, UserProfile>();
            CreateMap<UserProfile, UserProfileTransferDTO>();
        }
    }
}