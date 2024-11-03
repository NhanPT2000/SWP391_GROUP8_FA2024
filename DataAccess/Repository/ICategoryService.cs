using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid id);
        Task<bool> UpdateCategoryAsync(Category category, Guid id);
        Task<bool> DeleteCategoryAsync(Guid id);
        Task CreateCategoryAsync(Category category);
    }
}
