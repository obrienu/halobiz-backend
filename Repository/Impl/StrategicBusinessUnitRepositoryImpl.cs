using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl
{
    public class StrategicBusinessUnitRepositoryImpl : IStrategicBusinessUnitRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<StrategicBusinessUnitRepositoryImpl> _logger;
        public StrategicBusinessUnitRepositoryImpl(DataContext context, ILogger<StrategicBusinessUnitRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<StrategicBusinessUnit> SaveStrategyBusinessUnit(StrategicBusinessUnit sbu)
        {
            var savedEntity = await _context.StrategicBusinessUnits.AddAsync(sbu);
            if(await SaveChanges())
            {
                return savedEntity.Entity;
            }
            return null;
        }

        public async Task<StrategicBusinessUnit> FindStrategyBusinessUnitById(long Id)
        {
            return await _context.StrategicBusinessUnits
                .FirstOrDefaultAsync( sbu => sbu.Id == Id);
        }

        public async Task<StrategicBusinessUnit> FindStrategyBusinessUnitByName(string name)
        {
            return await _context.StrategicBusinessUnits
                .FirstOrDefaultAsync( sbu => sbu.Name == name);
        }

        public async Task<IEnumerable<StrategicBusinessUnit>> FindAllStrategyBusinessUnits()
        {
            return await _context.StrategicBusinessUnits
                .ToListAsync();
        }

        public async Task<StrategicBusinessUnit> UpdateStrategyBusinessUnit(StrategicBusinessUnit sbu)
        {
            var updatedEntity =  _context.StrategicBusinessUnits.Update(sbu);
            if(await SaveChanges())
            {
                return updatedEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteStrategyBusinessUnit(StrategicBusinessUnit sbu)
        {
            _context.StrategicBusinessUnits.Remove(sbu);
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