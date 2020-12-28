using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Model;

namespace HaloBiz.Repository
{
    public interface IStateRepository
    {
        Task<State> FindStateById(long Id);
        Task<State> FindStateByName(string name);
        Task<IEnumerable<State>> FindAllStates();
        
    }
}