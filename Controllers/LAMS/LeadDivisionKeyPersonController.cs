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
    public class LeadDivisionKeyPersonController : ControllerBase
    {
        private readonly ILeadDivisionKeyPersonService _LeadDivisionKeyPersonService;

        public LeadDivisionKeyPersonController(ILeadDivisionKeyPersonService LeadDivisionKeyPersonService)
        {
            this._LeadDivisionKeyPersonService = LeadDivisionKeyPersonService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetLeadKeyPeople()
        {
            var response = await _LeadDivisionKeyPersonService.GetAllLeadDivisionKeyPerson();
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var LeadDivisionKeyPerson = ((ApiOkResponse)response).Result;
            return Ok(LeadDivisionKeyPerson);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var response = await _LeadDivisionKeyPersonService.GetLeadDivisionKeyPersonById(id);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var LeadDivisionKeyPerson = ((ApiOkResponse)response).Result;
            return Ok(LeadDivisionKeyPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(long id, LeadDivisionKeyPersonReceivingDTO LeadDivisionKeyPersonReceiving)
        {
            var response = await _LeadDivisionKeyPersonService.UpdateLeadDivisionKeyPerson(HttpContext, id, LeadDivisionKeyPersonReceiving);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var LeadDivisionKeyPerson = ((ApiOkResponse)response).Result;
            return Ok(LeadDivisionKeyPerson);
        }
    }
}



        