using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs.LAMS;
using HaloBiz.DTOs.TransferDTOs.LAMS;
using HaloBiz.Helpers;
using HaloBiz.Model;
using HaloBiz.Model.LAMS;
using HaloBiz.MyServices.LAMS;
using HaloBiz.Repository;
using Microsoft.AspNetCore.Http;

namespace HaloBiz.MyServices.Impl.LAMS
{
    public class LeadKeyPersonServiceImpl : ILeadKeyPersonService
    {
        private readonly IModificationHistoryRepository _historyRepo;
        private readonly ILeadKeyPersonRepository _leadKeyPersonRepo;
        private readonly IMapper _mapper;

        public LeadKeyPersonServiceImpl(IModificationHistoryRepository historyRepo,  IMapper mapper, ILeadKeyPersonRepository leadKeyPersonRepo)
        {
            this._mapper = mapper;
            this._historyRepo = historyRepo;
            this._leadKeyPersonRepo = leadKeyPersonRepo;
        }

        public async Task<ApiResponse> AddLeadKeyPerson(HttpContext context, LeadKeyPersonReceivingDTO leadKeyPersonReceivingDTO)
        {
            var leadKeyPerson = _mapper.Map<LeadKeyPerson>(leadKeyPersonReceivingDTO);
            leadKeyPerson.CreatedById = context.GetLoggedInUserId();
            var savedLeadKeyPerson = await _leadKeyPersonRepo.SaveLeadKeyPerson(leadKeyPerson);
            if (savedLeadKeyPerson == null)
            {
                return new ApiResponse(500);
            }
            var leadKeyPersonTransferDTO = _mapper.Map<LeadKeyPersonTransferDTO>(leadKeyPerson);
            return new ApiOkResponse(leadKeyPersonTransferDTO);
        }

        public async Task<ApiResponse> GetAllLeadKeyPerson()
        {
            var leadKeyPersons = await _leadKeyPersonRepo.FindAllLeadKeyPerson();
            if (leadKeyPersons == null)
            {
                return new ApiResponse(404);
            }
            var leadKeyPersonTransferDTO = _mapper.Map<IEnumerable<LeadKeyPersonTransferDTO>>(leadKeyPersons);
            return new ApiOkResponse(leadKeyPersonTransferDTO);
        }

        public async Task<ApiResponse> GetLeadKeyPersonById(long id)
        {
            var leadKeyPerson = await _leadKeyPersonRepo.FindLeadKeyPersonById(id);
            if (leadKeyPerson == null)
            {
                return new ApiResponse(404);
            }
            var leadKeyPersonTransferDTO = _mapper.Map<LeadKeyPersonTransferDTO>(leadKeyPerson);
            return new ApiOkResponse(leadKeyPersonTransferDTO);
        }

        

        public async Task<ApiResponse> UpdateLeadKeyPerson(HttpContext context, long id, LeadKeyPersonReceivingDTO leadKeyPersonReceivingDTO)
        {
            var leadKeyPersonToUpdate = await _leadKeyPersonRepo.FindLeadKeyPersonById(id);
            if (leadKeyPersonToUpdate == null)
            {
                return new ApiResponse(404);
            }
            
            var summary = $"Initial details before change, \n {leadKeyPersonToUpdate.ToString()} \n" ;

            leadKeyPersonToUpdate.FirstName = leadKeyPersonReceivingDTO.FirstName;
            leadKeyPersonToUpdate.LastName = leadKeyPersonReceivingDTO.LastName;
            leadKeyPersonToUpdate.DateOfBirth = leadKeyPersonReceivingDTO.DateOfBirth;
            leadKeyPersonToUpdate.Designation = leadKeyPersonReceivingDTO.Designation;
            leadKeyPersonToUpdate.Email = leadKeyPersonReceivingDTO.Email;
            leadKeyPersonToUpdate.MobileNumber = leadKeyPersonReceivingDTO.MobileNumber;
            
            var updatedLeadKeyPerson = await _leadKeyPersonRepo.UpdateLeadKeyPerson(leadKeyPersonToUpdate);

            summary += $"Details after change, \n {updatedLeadKeyPerson.ToString()} \n";

            if (updatedLeadKeyPerson == null)
            {
                return new ApiResponse(500);
            }
            ModificationHistory history = new ModificationHistory(){
                ModelChanged = "LeadKeyPerson",
                ChangeSummary = summary,
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedLeadKeyPerson.Id
            };

            await _historyRepo.SaveHistory(history);

            var leadKeyPersonTransferDTOs = _mapper.Map<LeadKeyPersonTransferDTO>(updatedLeadKeyPerson);
            return new ApiOkResponse(leadKeyPersonTransferDTOs);

        }

    }
}