using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Data;
using HaloBiz.Model;
using Microsoft.EntityFrameworkCore;

namespace HaloBiz.Repository.Impl
{
    public class StateRepositoryImpl : IStateRepository
    {
        private readonly DataContext _context;
        public StateRepositoryImpl(DataContext context)
        {
            this._context = context;
        }

        public async Task<State> FindStateById(long Id)
        {
            return await _context.States.Include(state => state.LGAs)                 
                .FirstOrDefaultAsync( state => state.Id == Id);
        }

        public async Task<State> FindStateByName(string name)
        {
            return await _context.States
                .Include(state => state.LGAs) 
                .FirstOrDefaultAsync( state => state.Name == name);
        }

        public async Task<IEnumerable<State>> FindAllStates()
        {
            return await _context.States
                .Include(state => state.LGAs) 
                .ToListAsync();
        }

    }
}