using HaloBiz.Data;
using HaloBiz.Model.AccountsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.Repository.Impl
{
    public class AccountRepositoryImpl : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountRepositoryImpl> _logger;
        public AccountRepositoryImpl(DataContext context, ILogger<AccountRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;

        }

        public async Task<bool> DeleteAccount(Account Account)
        {
            Account.IsDeleted = true;
            _context.Accounts.Update(Account);
            return await SaveChanges();
        }

        public async Task<Account> FindAccountByAlias(long alias)
        {
            return await _context.Accounts.FirstOrDefaultAsync(account => account.Alias == alias && account.IsDeleted == false);

        }

        public async Task<Account> FindAccountById(long Id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(Account => Account.Id == Id && Account.IsDeleted == false);
        }

        public async Task<IEnumerable<Account>> FindAllAccounts()
        {
            return await _context.Accounts.Where(user => user.IsDeleted == false).ToListAsync();

        }

        public async Task<Account> SaveAccount(Account Account)
        {
            var AccountEntity = await _context.Accounts.AddAsync(Account);
            if (await SaveChanges())
            {
                return AccountEntity.Entity;
            }
            return null;
        }

        public async Task<Account> UpdateAccount(Account Account)
        {
            var AccountEntity = _context.Accounts.Update(Account);
            if (await SaveChanges())
            {
                return AccountEntity.Entity;
            }
            return null;
        }
        private async Task<bool> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
