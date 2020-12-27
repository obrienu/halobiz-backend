using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Model_;

namespace HaloBiz.Repository
{
    public interface IBranchRepository
    {

        Task<Branch> SaveUserProfile(Branch branch);

        Task<Branch> FindBranchById(long Id);

        Task<Branch> FindBranchByName(string name);

        Task<IEnumerable<Branch>> FindAllBranches();

        Task<Branch> UpdateBranch(Branch branch);

        Task<bool> DeleteBranch(Branch branch);

       
    }
}