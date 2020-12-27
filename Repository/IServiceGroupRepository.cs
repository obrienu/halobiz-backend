using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Model_;

namespace HaloBiz.Repository
{
    public interface IServiceGroupRepository
    {
        Task<ServiceGroup> SaveServiceGroup(ServiceGroup serviceGroup);

        Task<ServiceGroup> FindServiceGroupById(long Id);

        Task<ServiceGroup> FindServiceGroupByName(string name);

        Task<IEnumerable<ServiceGroup>> FindAllServiceGroups();

        Task<ServiceGroup> UpdateServiceGroup(ServiceGroup serviceGroup);

        Task<bool> DeleteServiceGroup(ServiceGroup serviceGroup);
    }
}