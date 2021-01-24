using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model.LAMS;
using HaloBiz.Repository.LAMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl.LAMS
{
    public class CustomerRepositoryImpl : ICustomerRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CustomerRepositoryImpl> _logger;
        public CustomerRepositoryImpl(DataContext context, ILogger<CustomerRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;

        }

        public async Task<Customer> SaveCustomer(Customer entity)
        {
            var CustomerEntity = await _context.Customers.AddAsync(entity);
            return CustomerEntity.Entity;
        }

        public async Task<Customer> FindCustomerById(long Id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(entity => entity.Id == Id && entity.IsDeleted == false);
        }

        public async Task<IEnumerable<Customer>> FindAllCustomer()
        {
            return await _context.Customers
                .Where(entity => entity.IsDeleted == false)
                .OrderByDescending(entity => entity.GroupName)
                .ToListAsync();
        }
        public async Task<Customer> FindCustomerByName(string name)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(entity => entity.GroupName == name && entity.IsDeleted == false);
        }

        public async Task<Customer> UpdateCustomer(Customer entity)
        {
            var CustomerEntity = _context.Customers.Update(entity);
            if (await SaveChanges())
            {
                return CustomerEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteCustomer(Customer entity)
        {
            entity.IsDeleted = true;
            _context.Customers.Update(entity);
            return await SaveChanges();
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
