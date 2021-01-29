using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model.AccountsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl
{
    public class ControlAccountRepositoryImpl : IControlAccountRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ControlAccountRepositoryImpl> _logger;
        public ControlAccountRepositoryImpl(DataContext context, ILogger<ControlAccountRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<ControlAccount> SaveControlAccount(ControlAccount controlAccount)
        {
            var controlAccountEntity = await _context.ControlAccounts.AddAsync(controlAccount);
            if(await SaveChanges())
            {
                return controlAccountEntity.Entity;
            }
            return null;
        }

        public async Task<ControlAccount> FindControlAccountById(long Id)
        {
            return await _context.ControlAccounts
                .Include(control => control.Accounts)
                .FirstOrDefaultAsync( controlAccount => controlAccount.Id == Id && controlAccount.IsDeleted == false);
        }

        public async Task<ControlAccount> FindControlAccountByName(string name)
        {
            return await _context.ControlAccounts
                .Include(control => control.Accounts)
                .FirstOrDefaultAsync( controlAccount => controlAccount.Caption == name && controlAccount.IsDeleted == false);
        }
        public async Task<ControlAccount> FindControlAccountByAlias(long name)
        {
            return await _context.ControlAccounts
                .Include(control => control.Accounts)
                .FirstOrDefaultAsync( controlAccount => controlAccount.Alias== name && controlAccount.IsDeleted == false);
        }

        public async Task<IEnumerable<ControlAccount>> FindAllControlAccount()
        {
            return await _context.ControlAccounts
                .Include(controlAccount => controlAccount.Accounts)
                .Where(controlAccount => controlAccount.IsDeleted == false)
                .OrderByDescending(controlAccount => controlAccount.CreatedAt)
                .ToListAsync();
        }

        public async Task<ControlAccount> UpdateControlAccount(ControlAccount controlAccount)
        {
            var controlAccountEntity =  _context.ControlAccounts.Update(controlAccount);
            if(await SaveChanges())
            {
                return controlAccountEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteControlAccount(ControlAccount controlAccount)
        {
            controlAccount.IsDeleted = true;
            _context.ControlAccounts.Update(controlAccount);
            return await SaveChanges();
        }
        public IQueryable<ControlAccount> GetControlAccountQueryable()
        {
            return _context.ControlAccounts.AsQueryable();
        }
        private async Task<bool> SaveChanges()
        {
           try{
               return  await _context.SaveChangesAsync() > 0;
           }catch(Exception ex)
           {
               _logger.LogError(ex.Message);
               return false;
           }
        }
    }
}