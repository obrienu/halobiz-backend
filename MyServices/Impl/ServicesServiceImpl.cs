using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.DTOs.TransferDTOs;
using HaloBiz.Helpers;
using HaloBiz.Model;
using HaloBiz.Model.ManyToManyRelationship;
using HaloBiz.Repository;
using Microsoft.AspNetCore.Http;

namespace HaloBiz.MyServices.Impl
{
    public class ServicesServiceImpl : IServicesService
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IMapper _mapper;
        private readonly IServiceRequiredServiceDocumentRepository _requiredServiceDocRepo;
        private readonly IDeleteLogRepository _deleteLogRepo;
        private readonly IModificationHistoryRepository _modificationRepo;
        private readonly IServiceRequredServiceQualificationElementRepository _reqServiceElementRepo;

        public ServicesServiceImpl(IServicesRepository servicesRepository, IMapper mapper, IServiceRequiredServiceDocumentRepository requiredServiceDocRepo, IDeleteLogRepository deleteLogRepo, IModificationHistoryRepository modificationRepo, IServiceRequredServiceQualificationElementRepository reqServiceElementRepo)
        {
            this._mapper = mapper;
            this._requiredServiceDocRepo = requiredServiceDocRepo;
            this._deleteLogRepo = deleteLogRepo;
            this._modificationRepo = modificationRepo;
            this._reqServiceElementRepo = reqServiceElementRepo;
            this._servicesRepository = servicesRepository;
        }

        public async Task<ApiResponse> AddService(ServicesReceivingDTO servicesReceivingDTO)
        {
            IList<ServiceRequiredServiceDocument> serviceRequiredServiceDocument = new List<ServiceRequiredServiceDocument>();
            IList<ServiceRequredServiceQualificationElement> serviceQualificationElements = new List<ServiceRequredServiceQualificationElement>();

            var service = _mapper.Map<Services>(servicesReceivingDTO);
            var savedService = await _servicesRepository.SaveService(service);
            if (savedService == null)
            {
                return new ApiResponse(500);
            }

            foreach (long id in servicesReceivingDTO.RequiredServiceFieldsId)
            {
                serviceQualificationElements.Add(new ServiceRequredServiceQualificationElement(){
                    ServicesId = savedService.Id,
                    RequredServiceQualificationElementId = id
                });
            }

            foreach (long id in servicesReceivingDTO.RequiredDocumentsId)
            {
                serviceRequiredServiceDocument.Add(new ServiceRequiredServiceDocument(){
                    ServicesId = savedService.Id,
                    RequiredServiceDocumentId = id
                });
            }

            var isFieldsSaved = await _reqServiceElementRepo.SaveRangeServiceRequredServiceQualificationElement(serviceQualificationElements);

            var isDocSaved = await _requiredServiceDocRepo.SaveRangeServiceRequiredServiceDocument(serviceRequiredServiceDocument);
            
            if(!isDocSaved) {
                await DeleteService(savedService.Id);
                return new ApiResponse(500);
            }

            if(!isFieldsSaved) {
                await DeleteService(savedService.Id);
                await _requiredServiceDocRepo.DeleteRangeServiceRequiredServiceDocument(serviceRequiredServiceDocument);
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

        public async Task<ApiResponse> UpdateServices(HttpContext context, long id, ServicesReceivingDTO serviceReceivingDTO)
        {
            var serviceToUpdate = await _servicesRepository.FindServicesById(id);
            if (serviceToUpdate == null)
            {
                return new ApiResponse(404);
            }
            var summary = $"Initial details before change, \n {serviceToUpdate.ToString()} \n";

            
            serviceToUpdate.Name = serviceReceivingDTO.Name;
            serviceToUpdate.Description = serviceReceivingDTO.Description;
            serviceToUpdate.ImageUrl = serviceReceivingDTO.ImageUrl;
            serviceToUpdate.TargetId = serviceReceivingDTO.TargetId;
            serviceToUpdate.UnitPrice = serviceReceivingDTO.UnitPrice;
            serviceToUpdate.ServiceTypeId = serviceReceivingDTO.ServiceTypeId;
           
            var updatedService = await _servicesRepository.UpdateServices(serviceToUpdate);

            if (updatedService == null)
            {
                return new ApiResponse(500);
            }

            summary += $"Details after change, \n {serviceToUpdate.ToString()} \n";

            ModificationHistory history = new ModificationHistory()
            {
                ModelChanged = "Service",
                ChangeSummary = summary,
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedService.Id
            };

            await _modificationRepo.SaveHistory(history);
            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(updatedService);
            return new ApiOkResponse(serviceTransferDTO);

        }
        public async Task<ApiResponse> ApproveService(HttpContext context, long id)
        {
            var serviceToUpdate = await _servicesRepository.FindServicesById(id);
            if (serviceToUpdate == null)
            {
                return new ApiResponse(404);
            }
            
            serviceToUpdate.PublishedApprovedStatus = true;
            serviceToUpdate.IsRequestedForPublish = true;
           
            var updatedService = await _servicesRepository.UpdateServices(serviceToUpdate);

            if (updatedService == null)
            {
                return new ApiResponse(500);
            }
            ModificationHistory history = new ModificationHistory()
            {
                ModelChanged = "Service",
                ChangeSummary = $"Service with ServiceId: {updatedService.Id} was approved by user with userid {context.GetLoggedInUserId()}",
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedService.Id
            };

            await _modificationRepo.SaveHistory(history);

            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(updatedService);
            return new ApiOkResponse(serviceTransferDTO);

        }

        public async Task<ApiResponse> RequestPublishService(HttpContext context, long id)
        {
            var serviceToUpdate = await _servicesRepository.FindServicesById(id);
            if (serviceToUpdate == null)
            {
                return new ApiResponse(404);
            }

            serviceToUpdate.IsRequestedForPublish = true;
           
            var updatedService = await _servicesRepository.UpdateServices(serviceToUpdate);

            if (updatedService == null)
            {
                return new ApiResponse(500);
            }

            ModificationHistory history = new ModificationHistory()
            {
                ModelChanged = "Service",
                ChangeSummary = $"User with userId {context.GetLoggedInUserId()} requested for Service with ServiceId: {updatedService.Id} to be published ",
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedService.Id
            };

            await _modificationRepo.SaveHistory(history);

            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(updatedService);
            return new ApiOkResponse(serviceTransferDTO);

        }
        public async Task<ApiResponse> DisapproveService(HttpContext context, long id)
        {
            var serviceToUpdate = await _servicesRepository.FindServicesById(id);
            if (serviceToUpdate == null)
            {
                return new ApiResponse(404);
            }
            
            serviceToUpdate.PublishedApprovedStatus = false;
            serviceToUpdate.IsRequestedForPublish = true;
           
            var updatedService = await _servicesRepository.UpdateServices(serviceToUpdate);

            if (updatedService == null)
            {
                return new ApiResponse(500);
            }
            ModificationHistory history = new ModificationHistory()
            {
                ModelChanged = "Service",
                ChangeSummary = $"Service with ServiceId: {updatedService.Id} was disapproved by user with userid {context.GetLoggedInUserId()}",
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedService.Id
            };

            await _modificationRepo.SaveHistory(history);

            var serviceTransferDTO = _mapper.Map<ServicesTransferDTO>(updatedService);
            return new ApiOkResponse(serviceTransferDTO);

        }

        public async Task<ApiResponse> DeleteService(HttpContext context, long id)
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

            DeleteLog deleteLog = new DeleteLog(){
                DeletedById = context.GetLoggedInUserId(),
                DeletedModelId = serviceToDelete.Id,
                ChangeSummary = $"Deleted service with Id: {serviceToDelete.Id}",

            };

            await _deleteLogRepo.SaveDeleteLog(deleteLog);

            return new ApiOkResponse(true);
        }
        public async Task<ApiResponse> DeleteService( long id)
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