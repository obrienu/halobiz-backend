using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl
{
    public class ServiceGroupRepositoryImpl : IServiceGroupRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ServiceGroupRepositoryImpl> _logger;
        public ServiceGroupRepositoryImpl(DataContext context, ILogger<ServiceGroupRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<ServiceGroup> SaveServiceGroup(ServiceGroup serviceGroup)
        {
            var savedEntity = await _context.ServiceGroups.AddAsync(serviceGroup);
            if(await SaveChanges())
            {
                return savedEntity.Entity;
            }
            return null;
        }

        public async Task<ServiceGroup> FindServiceGroupById(long Id)
        {
            return await _context.ServiceGroups
                .Include(serviceGroup => serviceGroup.ServiceCategories)                 
                .FirstOrDefaultAsync( serviceGroup => serviceGroup.Id == Id);
        }

        public async Task<ServiceGroup> FindServiceGroupByName(string name)
        {
            return await _context.ServiceGroups
                .Include(serviceGroup => serviceGroup.ServiceCategories) 
                .FirstOrDefaultAsync( serviceGroup => serviceGroup.Name == name);
        }

        public async Task<IEnumerable<ServiceGroup>> FindAllServiceGroups()
        {
            return await _context.ServiceGroups
                .Include(serviceGroup => serviceGroup.ServiceCategories)
                .ToListAsync();
        }

        public async Task<ServiceGroup> UpdateServiceGroup(ServiceGroup serviceGroup)
        {
            var updatedEntity =  _context.ServiceGroups.Update(serviceGroup);
            if(await SaveChanges())
            {
                return updatedEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteServiceGroup(ServiceGroup serviceGroup)
        {
            _context.ServiceGroups.Remove(serviceGroup);
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