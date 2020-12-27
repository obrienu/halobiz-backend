using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl
{
    public class ServiceCategoryRepositoryImpl : IServiceCategoryRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ServiceCategoryRepositoryImpl> _logger;
        public ServiceCategoryRepositoryImpl(DataContext context, ILogger<ServiceCategoryRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<ServiceCategory> SaveServiceCategory(ServiceCategory serviceCategory)
        {
            var savedEntity = await _context.ServiceCategories.AddAsync(serviceCategory);
            if(await SaveChanges())
            {
                return savedEntity.Entity;
            }
            return null;
        }

        public async Task<ServiceCategory> FindServiceCategoryById(long Id)
        {
            return await _context.ServiceCategories
                .Include(office => office.Services)                 
                .FirstOrDefaultAsync( category => category.Id == Id);
        }

        public async Task<ServiceCategory> FindServiceCategoryByName(string name)
        {
            return await _context.ServiceCategories
                .Include(category => category.Services)
                .FirstOrDefaultAsync( office => office.Name == name);
        }

        public async Task<IEnumerable<ServiceCategory>> FindAllServiceCategories()
        {
            return await _context.ServiceCategories
                .Include(category => category.Services)
                .ToListAsync();
        }

        public async Task<ServiceCategory> UpdateServiceCategory(ServiceCategory category)
        {
            var updatedEntity =  _context.ServiceCategories.Update(category);
            if(await SaveChanges())
            {
                return updatedEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteServiceCategory(ServiceCategory category)
        {
            _context.ServiceCategories.Remove(category);
            return await SaveChanges();
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