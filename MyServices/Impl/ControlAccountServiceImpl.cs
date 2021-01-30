using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaloBiz.Data;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.DTOs.TransferDTOs;
using HaloBiz.Helpers;
using HaloBiz.Model.AccountsModel;
using HaloBiz.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.MyServices.Impl
{
    public class ControlAccountServiceImpl : IControlAccountService
    {
        private readonly ILogger<ControlAccountServiceImpl> _logger;
        private readonly DataContext _context;
        private readonly IControlAccountRepository _controlAccountRepo;
        private readonly IMapper _mapper;

        public ControlAccountServiceImpl(ILogger<ControlAccountServiceImpl> logger, DataContext context, IControlAccountRepository controlAccountRepo, IMapper mapper)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._context = context;
            this._controlAccountRepo = controlAccountRepo;
        }

        public async Task<ApiResponse> AddControlAccount(HttpContext context, ControlAccountReceivingDTO controlAccountReceivingDTO)
        {

            await _context.Database.OpenConnectionAsync();
            try{
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.ControlAccounts ON");
                var controlAcc = _mapper.Map<ControlAccount>(controlAccountReceivingDTO);
                controlAcc.CreatedById = context.GetLoggedInUserId();
                long accountClassId = controlAccountReceivingDTO.AccountClassId;
                var controlQuerable = _controlAccountRepo.GetControlAccountQueryable();

                var  lastSavedControl = await _context.ControlAccounts.Where(control => control.AccountClassId == accountClassId)
                    .OrderBy(control => control.Id).LastOrDefaultAsync();
                if(lastSavedControl == null)
                {
                    controlAcc.Id = accountClassId + 10000;
                }else{
                    controlAcc.Id = lastSavedControl.Id + 10000;
                }
                var savedControlAccount = await _context.ControlAccounts.AddAsync(controlAcc);
                if (savedControlAccount == null)
                {
                    return new ApiResponse(500);
                }
                await _context.SaveChangesAsync();
                var controlAccountTransferDTOs = _mapper.Map<ControlAccountTransferDTO>(controlAcc);
                return new ApiOkResponse(controlAccountTransferDTOs);

            }catch(Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                return new ApiResponse(500);
            }finally{
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.ControlAccounts OFF");
                await _context.Database.CloseConnectionAsync();
            }

        }

        public async Task<ApiResponse> DeleteControlAccount(long id)
        {
            var controlAccountTodelete = await _controlAccountRepo.FindControlAccountById(id);
            if (controlAccountTodelete == null)
            {
                return new ApiResponse(404);
            }

            if (!await _controlAccountRepo.DeleteControlAccount(controlAccountTodelete))
            {
                return new ApiResponse(500);
            }

            return new ApiOkResponse(true);
        }

        public async Task<ApiResponse> GetControlAccountByAlias(long alias)
        {
            var controlAccount = await _controlAccountRepo.FindControlAccountByAlias(alias);
            if (controlAccount == null)
            {
                return new ApiResponse(404);
            }
            var controlAccountTransferDTOs = _mapper.Map<ControlAccountTransferDTO>(controlAccount);
            return new ApiOkResponse(controlAccountTransferDTOs);
        }
        public async Task<ApiResponse> GetControlAccountByCaption(string caption)
        {
            var controlAccount = await _controlAccountRepo.FindControlAccountByName(caption);
            if (controlAccount == null)
            {
                return new ApiResponse(404);
            }
            var controlAccountTransferDTOs = _mapper.Map<ControlAccountTransferDTO>(controlAccount);
            return new ApiOkResponse(controlAccountTransferDTOs);
        }

        public async Task<ApiResponse> GetControlAccountById(long id)
        {
            var controlAccount = await _controlAccountRepo.FindControlAccountById(id);
            if (controlAccount == null)
            {
                return new ApiResponse(404);
            }
            var controlAccountTransferDTOs = _mapper.Map<ControlAccountTransferDTO>(controlAccount);
            return new ApiOkResponse(controlAccountTransferDTOs);
        }

        public async Task<ApiResponse> GetAllControlAccounts()
        {
            var controlAccountes = await _controlAccountRepo.FindAllControlAccount();
            if (controlAccountes == null)
            {
                return new ApiResponse(404);
            }
            var controlAccountTransferDTOs = _mapper.Map<IEnumerable<ControlAccountTransferDTO>>(controlAccountes);
            return new ApiOkResponse(controlAccountTransferDTOs);
        }

        public async Task<ApiResponse> UpdateControlAccount(long id, ControlAccountReceivingDTO controlAccountReceivingDTO)
        {
            var controlAccountToUpdate = await _controlAccountRepo.FindControlAccountById(id);
            if (controlAccountToUpdate == null)
            {
                return new ApiResponse(404);
            }
            controlAccountToUpdate.Alias = controlAccountReceivingDTO.Alias;
            controlAccountToUpdate.Description = controlAccountReceivingDTO.Description;
            var updatedControlAccount = await _controlAccountRepo.UpdateControlAccount(controlAccountToUpdate);

            if (updatedControlAccount == null)
            {
                return new ApiResponse(500);
            }
            var controlAccountTransferDTOs = _mapper.Map<ControlAccountTransferDTO>(updatedControlAccount);
            return new ApiOkResponse(controlAccountTransferDTOs);
        }
    }
}