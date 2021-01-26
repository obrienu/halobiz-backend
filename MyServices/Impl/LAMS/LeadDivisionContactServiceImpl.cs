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
    public class LeadDivisionContactServiceImpl : ILeadDivisionContactService
    {
        private readonly IModificationHistoryRepository _historyRepo;
        private readonly ILeadDivisionContactRepository _LeadDivisionContactRepo;
        private readonly IMapper _mapper;

        public LeadDivisionContactServiceImpl(IModificationHistoryRepository historyRepo, IMapper mapper, ILeadDivisionContactRepository LeadDivisionContactRepo)
        {
            this._mapper = mapper;
            this._historyRepo = historyRepo;
            this._LeadDivisionContactRepo = LeadDivisionContactRepo;
        }

        public async Task<ApiResponse> AddLeadDivisionContact(HttpContext context, LeadDivisionContactReceivingDTO LeadDivisionContactReceivingDTO)
        {
            var LeadDivisionContact = _mapper.Map<LeadDivisionContact>(LeadDivisionContactReceivingDTO);
            LeadDivisionContact.CreatedById = context.GetLoggedInUserId();
            var savedLeadDivisionContact = await _LeadDivisionContactRepo.SaveLeadDivisionContact(LeadDivisionContact);
            if (savedLeadDivisionContact == null)
            {
                return new ApiResponse(500);
            }
            var LeadDivisionContactTransferDTO = _mapper.Map<LeadDivisionContactTransferDTO>(LeadDivisionContact);
            return new ApiOkResponse(LeadDivisionContactTransferDTO);
        }

        public async Task<ApiResponse> GetAllLeadDivisionContact()
        {
            var LeadDivisionContacts = await _LeadDivisionContactRepo.FindAllLeadDivisionContact();
            if (LeadDivisionContacts == null)
            {
                return new ApiResponse(404);
            }
            var LeadDivisionContactTransferDTO = _mapper.Map<IEnumerable<LeadDivisionContactTransferDTO>>(LeadDivisionContacts);
            return new ApiOkResponse(LeadDivisionContactTransferDTO);
        }

        public async Task<ApiResponse> GetLeadDivisionContactById(long id)
        {
            var LeadDivisionContact = await _LeadDivisionContactRepo.FindLeadDivisionContactById(id);
            if (LeadDivisionContact == null)
            {
                return new ApiResponse(404);
            }
            var LeadDivisionContactTransferDTO = _mapper.Map<LeadDivisionContactTransferDTO>(LeadDivisionContact);
            return new ApiOkResponse(LeadDivisionContactTransferDTO);
        }



        public async Task<ApiResponse> UpdateLeadDivisionContact(HttpContext context, long id, LeadDivisionContactReceivingDTO LeadDivisionContactReceivingDTO)
        {
            var LeadDivisionContactToUpdate = await _LeadDivisionContactRepo.FindLeadDivisionContactById(id);
            if (LeadDivisionContactToUpdate == null)
            {
                return new ApiResponse(404);
            }

            var summary = $"Initial details before change, \n {LeadDivisionContactToUpdate.ToString()} \n";

            LeadDivisionContactToUpdate.FirstName = LeadDivisionContactReceivingDTO.FirstName;
            LeadDivisionContactToUpdate.LastName = LeadDivisionContactReceivingDTO.LastName;
            LeadDivisionContactToUpdate.DateOfBirth = LeadDivisionContactReceivingDTO.DateOfBirth;
            LeadDivisionContactToUpdate.Designation = LeadDivisionContactReceivingDTO.Designation;
            LeadDivisionContactToUpdate.Email = LeadDivisionContactReceivingDTO.Email;
            LeadDivisionContactToUpdate.MobileNumber = LeadDivisionContactReceivingDTO.MobileNumber;

            var updatedLeadDivisionContact = await _LeadDivisionContactRepo.UpdateLeadDivisionContact(LeadDivisionContactToUpdate);

            summary += $"Details after change, \n {updatedLeadDivisionContact.ToString()} \n";

            if (updatedLeadDivisionContact == null)
            {
                return new ApiResponse(500);
            }
            ModificationHistory history = new ModificationHistory()
            {
                ModelChanged = "LeadDivisionContact",
                ChangeSummary = summary,
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedLeadDivisionContact.Id
            };

            await _historyRepo.SaveHistory(history);

            var LeadDivisionContactTransferDTOs = _mapper.Map<LeadDivisionContactTransferDTO>(updatedLeadDivisionContact);
            return new ApiOkResponse(LeadDivisionContactTransferDTOs);

        }

    }
}
