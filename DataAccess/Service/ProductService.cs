using DataAccess.Database;
using DataAccess.Repository;
using DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task CreateProductAsync(Product product)
        {
            using var transaction = await _productContext.Database.BeginTransactionAsync();
            try
            {
                var newProduct = new Product
                {
                    CategoryId = product.CategoryId,
                    ProductDescription = product.ProductDescription,
                    Image = product.Image,
                    Origin = product.Origin,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    Weight = product.Weight,
                };

                await _productContext.Products.AddAsync(newProduct);
                await _productContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var productToDelete = await _productContext.Products.Include(p => p._OrderDetails).FirstOrDefaultAsync(p => p.ProductId == id);

            if (productToDelete == null)
            {
                return false;
            }
            if(productToDelete._OrderDetails != null && productToDelete._OrderDetails.Any()) productToDelete.IsDeleted = true;
            else _productContext.Remove(productToDelete);
            return await _productContext.SaveChangesAsync() > 0;
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
                .Where(p => p.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product, Guid id)
        {
            var productToUpdate = await _productContext.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return false;
            }
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.ProductDescription = product.ProductDescription;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Image = product.Image;
            productToUpdate.Origin = product.Origin;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.Weight = product.Weight;
            productToUpdate.CategoryId = product.CategoryId;
            _productContext.Update(productToUpdate);
            return await _productContext.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Product>> SearchProduct(Expression<Func<Product, bool>> filter)
        {
            return await _productContext.
                Products
                .Include(p => p.Category)
                .Where(filter)
                .ToListAsync();
        }
    }
}
