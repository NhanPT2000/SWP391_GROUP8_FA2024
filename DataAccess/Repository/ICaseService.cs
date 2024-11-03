using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICaseService
    {
        Task<IEnumerable<Case>> GetCasesAsync();
        Task<Case?> GetCaseByIdAsync(Guid id);
        Task<bool> UpdateCategoryAsync(Case _case, Guid id);
        Task<bool> DeleteCaseAsync(Guid id);
        Task CreateCaseAsync(Case _case);
    }
}
