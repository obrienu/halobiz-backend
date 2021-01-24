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
using HaloBiz.Repository.LAMS;
using Microsoft.AspNetCore.Http;

namespace HaloBiz.MyServices.Impl.LAMS
{
    public class LeadContactServiceImpl : ILeadContactService
    {
        private readonly IModificationHistoryRepository _historyRepo;
        private readonly ILeadContactRepository _leadContactRepo;
        private readonly IMapper _mapper;

        public LeadContactServiceImpl(IModificationHistoryRepository historyRepo,  IMapper mapper, ILeadContactRepository leadContactRepo)
        {
            this._mapper = mapper;
            this._historyRepo = historyRepo;
            this._leadContactRepo = leadContactRepo;
        }

        public async Task<ApiResponse> AddLeadContact(HttpContext context, LeadContactReceivingDTO leadContactReceivingDTO)
        {
            var leadContact = _mapper.Map<LeadContact>(leadContactReceivingDTO);
            leadContact.CreatedById = context.GetLoggedInUserId();
            var savedLeadContact = await _leadContactRepo.SaveLeadContact(leadContact);
            if (savedLeadContact == null)
            {
                return new ApiResponse(500);
            }
            var leadContactTransferDTO = _mapper.Map<LeadContactTransferDTO>(leadContact);
            return new ApiOkResponse(leadContactTransferDTO);
        }

        public async Task<ApiResponse> GetAllLeadContact()
        {
            var leadContacts = await _leadContactRepo.FindAllLeadContact();
            if (leadContacts == null)
            {
                return new ApiResponse(404);
            }
            var leadContactTransferDTO = _mapper.Map<IEnumerable<LeadContactTransferDTO>>(leadContacts);
            return new ApiOkResponse(leadContactTransferDTO);
        }

        public async Task<ApiResponse> GetLeadContactById(long id)
        {
            var leadContact = await _leadContactRepo.FindLeadContactById(id);
            if (leadContact == null)
            {
                return new ApiResponse(404);
            }
            var leadContactTransferDTO = _mapper.Map<LeadContactTransferDTO>(leadContact);
            return new ApiOkResponse(leadContactTransferDTO);
        }

        

        public async Task<ApiResponse> UpdateLeadContact(HttpContext context, long id, LeadContactReceivingDTO leadContactReceivingDTO)
        {
            var leadContactToUpdate = await _leadContactRepo.FindLeadContactById(id);
            if (leadContactToUpdate == null)
            {
                return new ApiResponse(404);
            }
            
            var summary = $"Initial details before change, \n {leadContactToUpdate.ToString()} \n" ;

            leadContactToUpdate.FirstName = leadContactReceivingDTO.FirstName;
            leadContactToUpdate.LastName = leadContactReceivingDTO.LastName;
            leadContactToUpdate.DateOfBirth = leadContactReceivingDTO.DateOfBirth;
            leadContactToUpdate.Designation = leadContactReceivingDTO.Designation;
            leadContactToUpdate.Email = leadContactReceivingDTO.Email;
            leadContactToUpdate.MobileNumber = leadContactReceivingDTO.MobileNumber;
            
            var updatedLeadContact = await _leadContactRepo.UpdateLeadContact(leadContactToUpdate);

            summary += $"Details after change, \n {updatedLeadContact.ToString()} \n";

            if (updatedLeadContact == null)
            {
                return new ApiResponse(500);
            }
            ModificationHistory history = new ModificationHistory(){
                ModelChanged = "LeadContact",
                ChangeSummary = summary,
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedLeadContact.Id
            };

            await _historyRepo.SaveHistory(history);

            var leadContactTransferDTOs = _mapper.Map<LeadContactTransferDTO>(updatedLeadContact);
            return new ApiOkResponse(leadContactTransferDTOs);

        }

    }
}