using AutoMapper;
using HaloBiz.DTOs;
using HaloBiz.Model_;

namespace HaloBiz.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<State, StateTransferDTO>();    
            CreateMap<LGA, LGATransferDTO>();     
        }
    }
}