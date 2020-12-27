using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HaloBiz.Repository.Impl
{
    public class OfficesRepositoryImpl : IOfficesRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<OfficesRepositoryImpl> _logger;
        public OfficesRepositoryImpl(DataContext context, ILogger<OfficesRepositoryImpl> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<Office> SaveOffice(Office office)
        {
            var officeEntity = await _context.Offices.AddAsync(office);
            if(await SaveChanges())
            {
                return officeEntity.Entity;
            }
            return null;
        }

        public async Task<Office> FindOfficeById(long Id)
        {
            return await _context.Offices
                .Include(office => office.Head)
                .Include(office => office.State)
                .Include(office => office.LGA)                  
                .FirstOrDefaultAsync( office => office.Id == Id);
        }

        public async Task<Office> FindOfficeByName(string name)
        {
            return await _context.Offices
                .Include(office => office.Head)
                .Include(office => office.State)
                .Include(office => office.LGA)
                .FirstOrDefaultAsync( office => office.Name == name);
        }

        public async Task<IEnumerable<Office>> FindAllOffices()
        {
            return await _context.Offices
                .Include(office => office.Head)
                .Include(office => office.State)
                .Include(office => office.LGA)
                .ToListAsync();
        }

        public async Task<Office> UpdateOffice(Office office)
        {
            var officeEntity =  _context.Offices.Update(office);
            if(await SaveChanges())
            {
                return officeEntity.Entity;
            }
            return null;
        }

        public async Task<bool> DeleteOffice(Office office)
        {
            _context.Offices.Remove(office);
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