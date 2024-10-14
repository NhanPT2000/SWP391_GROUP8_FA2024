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
    public class ProductService : IProductService
    {
        private readonly PetShopContext _productContext;
        public ProductService(PetShopContext dbContext)
        {
            _productContext = dbContext;
        }
        public Task CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            var product = await _productContext.Products.FindAsync(id);

            if (product == null)
            {
                return default;
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productContext.
                Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public Task<bool> UpdateProductAsync(Product product, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
