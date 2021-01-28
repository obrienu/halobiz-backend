using System.Threading.Tasks;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs.LAMS;
using HaloBiz.Model.LAMS;
using Microsoft.AspNetCore.Http;

namespace HaloBiz.MyServices.LAMS
{
    public interface ILeadService
    {
        Task<ApiResponse> AddLead(HttpContext context, LeadReceivingDTO leadReceivingDTO);
        Task<ApiResponse> GetAllLead();
        Task<ApiResponse> GetLeadById(long id);
        Task<ApiResponse> GetLeadByReferenceNumber(string refNumber);
        Task<ApiResponse> UpdateLead(HttpContext context, long id, LeadReceivingDTO leadReceivingDTO);
    }
}