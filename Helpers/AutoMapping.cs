using System;
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
            CreateMap<UserProfileReceivingDTO, UserProfile>()
                .ForMember(member => member.DateOfBirth, 
                    opt =>  opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)));
            CreateMap<UserProfile, UserProfileTransferDTO>()
                .ForMember(member => member.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));
            CreateMap<Branch, BranchTransferDTO>();
            CreateMap<BranchReceivingDTO, Branch>();
            CreateMap<Division, DivisionTransferDTO>();
            CreateMap<DivisionReceivingDTO, Division>();
            CreateMap<OperatingEntity, OperatingEntityTransferDTO>();
            CreateMap<OperatingEntityReceivingDTO, OperatingEntity>();
            CreateMap<State, StateWithoutLGATransferDto>();
            CreateMap<OfficeReceivingDTO, Office>();
            CreateMap<Office, OfficeTransferDTO>();
            CreateMap<ServiceGroup, ServiceGroupTransferDTO>();
            CreateMap<ServiceGroupReceivingDTO, ServiceGroup>();
            CreateMap<ServiceCategoryReceivingDTO, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryTransferDTO>();
            CreateMap<Services, ServicesTransferDTO>();
            CreateMap<ServicesReceivingDTO, Services>();
        }
    }
}