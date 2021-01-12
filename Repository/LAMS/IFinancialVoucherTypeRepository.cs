using System.Collections.Generic;
using System.Threading.Tasks;
using HaloBiz.Model.LAMS;

namespace HaloBiz.Repository.LAMS
{
    public interface IFinancialVoucherTypeRepository
    {
         Task<FinanceVoucherType> SaveFinanceVoucherType(FinanceVoucherType leadOrigin);
         Task<FinanceVoucherType> FindFinanceVoucherTypeById(long Id);
        Task<IEnumerable<FinanceVoucherType>> FindAllFinanceVoucherTypes();
         Task<FinanceVoucherType> UpdateFinanceVoucherType(FinanceVoucherType leadOrigin);
         Task<bool> DeleteFinanceVoucherType(FinanceVoucherType leadOrigin);
    }
}