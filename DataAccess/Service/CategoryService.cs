using DataAccess.Database;
using DataAccess.Repository;
using DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly PetShopContext _context;

        public CategoryService(PetShopContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newCategory = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = category.CategoryName,
                    Status = category.Status
                };

                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var categoryToDelete = await _context.Categories.Include(c => c._Products).FirstOrDefaultAsync(c => c.CategoryId == id);
            if (categoryToDelete == null) return false;
            if (categoryToDelete._Products != null && categoryToDelete._Products.Any()) categoryToDelete.IsDeleted = true;
            else _context.Categories.Remove(categoryToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories
                .Include(c => c._Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c._Products)
                .Where(c => c.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Category category, Guid id)
        {
            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (categoryToUpdate == null) return false;

            categoryToUpdate.CategoryName = category.CategoryName;
            categoryToUpdate.Status = category.Status;

            _context.Categories.Update(categoryToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
