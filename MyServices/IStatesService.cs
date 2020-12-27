using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.DTOs;
using HaloBiz.Model_;

namespace HaloBiz.MyServices
{
    public interface IStatesService
    {
        Task<ApiResponse> GetStateById(long id);
        Task<ApiResponse> GetStateByName(string name);
        Task<ApiResponse> GetAllStates();
    }
}