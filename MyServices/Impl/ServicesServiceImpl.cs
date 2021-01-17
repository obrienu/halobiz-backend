using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.DTOs.TransferDTOs;
using HaloBiz.Model;
using HaloBiz.Repository;

namespace HaloBiz.MyServices.Impl
{
    public class ServicesServiceImpl : IServicesService
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IMapper _mapper;

        public ServicesServiceImpl(IServicesRepository servicesRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._servicesRepository = servicesRepository;
        }

        public async Task<ApiResponse> AddService(ServicesReceivingDTO servicesReceivingDTO)
        {
            var service = _mapper.Map<Services>(servicesReceivingDTO);
            var savedService = await _servicesRepository.SaveService(service);
            if (savedService == null)
            {
                return new ApiResponse(500);
            }
            var servicesTransferDTO = _mapper.Map<ServicesTransferDTO>(savedService);
            return new ApiOkResponse(servicesTransferDTO);
        }

        public async Task<ApiResponse> GetAllServices()
        {
            var services = await _servicesRepository.FindAllServices();
            if (services == null)
            {
                return new ApiResponse(404);
            }
            var serviceTransferDTO = _mapper.Map<IEnumerable<ServicesTransferDTO>>(services);
            return new ApiOkResponse(serviceTransferDTO);
        }

        public async Task<ApiResponse> GetServiceById(long id)
        {
            var service = await _servicesRepository.FindServicesById(id);
            if (service == null)
            {
                return new ApiResponse(404);
            }
            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(service);
            return new ApiOkResponse(serviceTransferDTO);
        }

        public async Task<ApiResponse> GetServiceByName(string name)
        {
            var service = await _servicesRepository.FindServiceByName(name);
            if (service == null)
            {
                return new ApiResponse(404);
            }
            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(service);
            return new ApiOkResponse(serviceTransferDTO);
        }

        public async Task<ApiResponse> UpdateServices(long id, ServicesReceivingDTO serviceReceivingDTO)
        {
            var serviceToUpdate = await _servicesRepository.FindServicesById(id);
            if (serviceToUpdate == null)
            {
                return new ApiResponse(404);
            }
            serviceToUpdate.Name = serviceReceivingDTO.Name;
            serviceToUpdate.Description = serviceReceivingDTO.Description;
            serviceToUpdate.ImageUrl = serviceReceivingDTO.ImageUrl;
            serviceToUpdate.TargetId = serviceReceivingDTO.TargetId;
            serviceToUpdate.UnitPrice = serviceReceivingDTO.UnitPrice;
            serviceToUpdate.DivisionId = serviceReceivingDTO.DivisionId;
            serviceToUpdate.OperatingEntityId = serviceReceivingDTO.OperatingEntityId;
            serviceToUpdate.ServiceCategoryId = serviceReceivingDTO.ServiceCategoryId;
            serviceToUpdate.ServiceGroupId = serviceReceivingDTO.ServiceGroupId;
            var updatedService = await _servicesRepository.UpdateServices(serviceToUpdate);

            if (updatedService == null)
            {
                return new ApiResponse(500);
            }
            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(updatedService);
            return new ApiOkResponse(serviceTransferDTO);

        }

        public async Task<ApiResponse> DeleteService(long id)
        {
            var serviceToDelete = await _servicesRepository.FindServicesById(id);
            if (serviceToDelete == null)
            {
                return new ApiResponse(404);
            }

            if (!await _servicesRepository.DeleteService(serviceToDelete))
            {
                return new ApiResponse(500);
            }

            return new ApiOkResponse(true);
        }
    }
}