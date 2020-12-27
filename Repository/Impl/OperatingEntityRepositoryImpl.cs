using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl
{
    public class OperatingEntityRepositoryImpl : IOperatingEntityRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<OperatingEntityRepositoryImpl> _logger;
        public OperatingEntityRepositoryImpl(DataContext context, ILogger<OperatingEntityRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;

        }

        public async Task<OperatingEntity> SaveOperatingEntity(OperatingEntity operatingEntity)
        {
            var savedEntity = await _context.OperatingEntities
                .AddAsync(operatingEntity);
                
            if(await SaveChanges())
            {
                return savedEntity.Entity;
            }
            return null;
        }

        public async Task<OperatingEntity> FindOperatingEntityById(long Id)
        {
            return await _context.OperatingEntities
                .Include(operatingEntity => operatingEntity.Head)
                .Include(operatingEntity => operatingEntity.ServiceGroups)
                .Include(operatingEntity => operatingEntity.StrategicBusinessUnits)        
                .FirstOrDefaultAsync( operatingEntity => operatingEntity.Id == Id);
        }

        public async Task<OperatingEntity> FindOfficeByName(string name)
        {
            return await _context.OperatingEntities
                .Include(operatingEntity => operatingEntity.Head)
                .Include(operatingEntity => operatingEntity.ServiceGroups)
                .Include(operatingEntity => operatingEntity.StrategicBusinessUnits)        
                .FirstOrDefaultAsync( operatingEntity => operatingEntity.Name == name);
        }

        public async Task<IEnumerable<OperatingEntity>> FindAllOperatingEntity()
        {
            return await _context.OperatingEntities
                .Include(operatingEntity => operatingEntity.Head)
                .Include(operatingEntity => operatingEntity.ServiceGroups)
                .Include(operatingEntity => operatingEntity.StrategicBusinessUnits)   
                .ToListAsync();
        }

        public async Task<OperatingEntity> UpdateOperatingEntity(OperatingEntity operatingEntity)
        {
            var updatedEntity =  _context.OperatingEntities.Update(operatingEntity);
            if(await SaveChanges())
            {
                return updatedEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteOperatingEntity(OperatingEntity operatingEntity)
        {
            _context.OperatingEntities.Remove(operatingEntity);
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