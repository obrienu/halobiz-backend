using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Model;

namespace HaloBiz.Repository
{
    public interface IOperatingEntityRepository
    {
        Task<OperatingEntity> SaveOperatingEntity(OperatingEntity operatingEntity);

        Task<OperatingEntity> FindOperatingEntityById(long Id);

        Task<OperatingEntity> FindOfficeByName(string name);

        Task<IEnumerable<OperatingEntity>> FindAllOperatingEntity();

        Task<OperatingEntity> UpdateOperatingEntity(OperatingEntity operatingEntity);

        Task<bool> DeleteOperatingEntity(OperatingEntity operatingEntity);

    }
}