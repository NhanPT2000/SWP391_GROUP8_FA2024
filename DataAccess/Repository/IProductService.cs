using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<bool> UpdateProductAsync(Product product, Guid id);
        Task<bool> DeleteProductAsync(Guid id);
        Task CreateProductAsync(Product product);
    }
}
