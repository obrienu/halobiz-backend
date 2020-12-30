using AutoMapper;
using HaloBiz.DTOs.ReceivingDTO;
using HaloBiz.DTOs.ReceivingDTOs;
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
            CreateMap<Branch, BranchTransferDTO>();
            CreateMap<BranchReceivingDTO, Branch>();
            CreateMap<Division, DivisionTransferDTO>();
            CreateMap<DivisionReceivingDTO, Division>();
            CreateMap<OperatingEntity, OperatingEntityTransferDTO>();
            CreateMap<OperatingEntityReceivingDTO, OperatingEntity>();
            CreateMap<State, StateWithoutLGATransferDto>();
            CreateMap<OfficeReceivingDTO, Office>();
            CreateMap<Office, OfficeTransferDTO>();
            CreateMap<StrategicBusinessUnit, StrategicBusinessUnitTransferDTO>();
            CreateMap<StrategicBusinessUnitReceivingDTO, StrategicBusinessUnit>();
        }
    }
}