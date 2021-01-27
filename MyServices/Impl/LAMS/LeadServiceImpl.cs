using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HaloBiz.Data;
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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace HaloBiz.MyServices.Impl.LAMS
{
    public class LeadServiceImpl : ILeadService
    {
        private readonly ILogger<LeadServiceImpl> _logger;
        private readonly IModificationHistoryRepository _historyRepo;
        private readonly ILeadRepository _leadRepo;
        private readonly IMapper _mapper;

        public LeadServiceImpl(
                                IModificationHistoryRepository historyRepo,
                                ILeadRepository leadRepo,
                                ILogger<LeadServiceImpl> logger,
                                IMapper mapper
                                )
        {
            this._mapper = mapper;
            this._historyRepo = historyRepo;
            this._leadRepo = leadRepo;
            this._logger = logger;
        }

        public async Task<ApiResponse> AddLead(HttpContext context, LeadReceivingDTO leadReceivingDTO)
        {
            var lead = _mapper.Map<Lead>(leadReceivingDTO);
            lead.CreatedById = context.GetLoggedInUserId();
            var savedLead = await _leadRepo.SaveLead(lead);
            if (savedLead == null)
            {
                return new ApiResponse(500);
            }
            var leadTransferDTO = _mapper.Map<LeadTransferDTO>(savedLead);
            return new ApiOkResponse(leadTransferDTO);
        }

        public async Task<ApiResponse> GetAllLead()
        {
            var leads = await _leadRepo.FindAllLead();
            if (leads == null)
            {
                return new ApiResponse(404);
            }
            var leadTransferDTO = _mapper.Map<IEnumerable<LeadTransferDTO>>(leads);
            return new ApiOkResponse(leadTransferDTO);
        }

        public async Task<ApiResponse> GetLeadById(long id)
        {
            var lead = await _leadRepo.FindLeadById(id);
            if (lead == null)
            {
                return new ApiResponse(404);
            }
            var leadTransferDTOs = _mapper.Map<LeadTransferDTO>(lead);
            return new ApiOkResponse(leadTransferDTOs);
        }

        public async Task<ApiResponse> GetLeadByReferenceNumber(string refNumber)
        {
            var lead = await _leadRepo.FindLeadByReferenceNo(refNumber);
            if (lead == null)
            {
                return new ApiResponse(404);
            }
            var leadTransferDTOs = _mapper.Map<LeadTransferDTO>(lead);
            return new ApiOkResponse(leadTransferDTOs);
        }

        public async Task<ApiResponse> UpdateLead(HttpContext context, long id, LeadReceivingDTO leadReceivingDTO)
        {

            var leadToUpdate = await _leadRepo.FindLeadById(id);
            if (leadToUpdate == null)
            {
                return new ApiResponse(404);
            }

            var summary = $"Initial details before change, \n {leadToUpdate.ToString()} \n";

            leadToUpdate.LeadTypeId = leadReceivingDTO.LeadTypeId;
            leadToUpdate.LeadOriginId = leadReceivingDTO.LeadOriginId;
            leadToUpdate.Industry = leadReceivingDTO.Industry;
            leadToUpdate.RCNumber = leadReceivingDTO.RCNumber;
            leadToUpdate.GroupName = leadReceivingDTO.GroupName;
            leadToUpdate.GroupTypeId = leadReceivingDTO.GroupTypeId;
            leadToUpdate.LogoUrl = leadReceivingDTO.LogoUrl;

            var updatedLead = await _leadRepo.UpdateLead(leadToUpdate);

            if(updatedLead == null)
            {
                return new ApiResponse(500);
            }

            summary += $"Details after change, \n {updatedLead.ToString()} \n";

            ModificationHistory history = new ModificationHistory()
            {
                ModelChanged = "Lead",
                ChangeSummary = summary,
                ChangedById = context.GetLoggedInUserId(),
                ModifiedModelId = updatedLead.Id
            };

            await _historyRepo.SaveHistory(history);

            var leadTransferDTOs = _mapper.Map<LeadTransferDTO>(updatedLead);
            return new ApiOkResponse(leadTransferDTOs);


        }

    }
}

