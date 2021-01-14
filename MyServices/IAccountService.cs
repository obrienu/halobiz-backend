using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.MyServices
{
    public interface IAccountService
    {
        Task<ApiResponse> AddAccount(AccountReceivingDTO accountClassReceivingDTO);
        Task<ApiResponse> GetAccountById(long id);
        Task<ApiResponse> GetAccountByAlias(long alias);
        Task<ApiResponse> GetAllAccounts();
        Task<ApiResponse> UpdateAccount(long id, AccountReceivingDTO accountClassReceivingDTO);
        Task<ApiResponse> DeleteAccount(long id);
    }
}
