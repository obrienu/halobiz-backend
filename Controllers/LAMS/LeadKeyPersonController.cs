using System.Threading.Tasks;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.DTOs.ReceivingDTOs.LAMS;
using HaloBiz.MyServices.LAMS;
using Microsoft.AspNetCore.Mvc;

namespace HaloBiz.Controllers.LAMS
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeadKeyPersonController : ControllerBase
    {
        private readonly ILeadKeyPersonService _leadKeyPersonService;

        public LeadKeyPersonController(ILeadKeyPersonService leadKeyPersonService)
        {
            this._leadKeyPersonService = leadKeyPersonService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetLeadKeyPeople()
        {
            var response = await _leadKeyPersonService.GetAllLeadKeyPerson();
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var leadKeyPerson = ((ApiOkResponse)response).Result;
            return Ok(leadKeyPerson);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var response = await _leadKeyPersonService.GetLeadKeyPersonById(id);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var leadKeyPerson = ((ApiOkResponse)response).Result;
            return Ok(leadKeyPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(long id, LeadKeyPersonReceivingDTO leadKeyPersonReceiving)
        {
            var response = await _leadKeyPersonService.UpdateLeadKeyPerson(HttpContext, id, leadKeyPersonReceiving);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var leadKeyPerson = ((ApiOkResponse)response).Result;
            return Ok(leadKeyPerson);
        }
    }
}



        