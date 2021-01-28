using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.DTOs.ReceivingDTOs.LAMS;
using HaloBiz.DTOs.TransferDTOs.LAMS;
using HaloBiz.MyServices.LAMS;
using Microsoft.AspNetCore.Mvc;

namespace HaloBiz.Controllers.LAMS
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadController(ILeadService leadService)
        {
            this._leadService = leadService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetLead()
        {
            var response = await _leadService.GetAllLead();
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var lead = ((ApiOkResponse)response).Result;
            return Ok(lead);
        }

        [HttpGet("ReferenceNumber/{refNo}")]
        public async Task<ActionResult> GetByCaption(string refNo)
        {
            var response = await _leadService.GetLeadByReferenceNumber(refNo);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var lead = ((ApiOkResponse)response).Result;
            return Ok(lead);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var response = await _leadService.GetLeadById(id);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var lead = ((ApiOkResponse)response).Result;
            return Ok(lead);
        }

        [HttpPost("")]
        public async Task<ActionResult> AddNewLead(LeadReceivingDTO leadReceiving)
        {
            var response = await _leadService.AddLead(HttpContext, leadReceiving);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var lead = ((ApiOkResponse)response).Result;
            return Ok(lead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(long id, LeadReceivingDTO leadReceivingDTO)
        {
            var response = await _leadService.UpdateLead(HttpContext, id, leadReceivingDTO);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var lead = ((ApiOkResponse)response).Result;
            return Ok(lead);
        }
    }
}