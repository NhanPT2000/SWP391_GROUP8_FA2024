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
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly PetShopContext _serviceContext;
        public OrderDetailsService(PetShopContext dbContext)
        {
            _serviceContext = dbContext;
        }
        public async Task CreateOrderDetailsAsync(OrderDetails orderDetails)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            float price = 0;
            var product = await _serviceContext.Products.SingleOrDefaultAsync(p => p.ProductId == orderDetails.ProductId);
            if (product != null) 
            {
                price = (float)(orderDetails.Quantity * product.UnitPrice - orderDetails.Discount);
            }
            try
            {
                var newOrderDetails = new OrderDetails
                {
                    Discount = orderDetails.Discount,
                    OrderId = orderDetails.OrderId,
                    ProductId = orderDetails.ProductId,
                    Quantity = orderDetails.Quantity,
                    UnitPrice = price
                };

                await _serviceContext._OrderDetails.AddAsync(newOrderDetails);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteOrderDetailsAsync(Guid orderId, Guid productId)
        {
            var orderDetailsToDelete = await _serviceContext._OrderDetails.Include(o => o.Order).FirstOrDefaultAsync(p => p.OrderId == orderId && p.ProductId == orderId);

            if (orderDetailsToDelete == null)
            {
                return false;
            }
            if(orderDetailsToDelete.Order != null) orderDetailsToDelete.IsDeleted = true;
            else _serviceContext.Remove(orderDetailsToDelete);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<OrderDetails?> GetOrderDetailsByIdAsync(Guid orderId, Guid productId)
        {
            var orderDetails = await _serviceContext._OrderDetails.FindAsync([orderId, productId]);

            if (orderDetails == null)
            {
                return default;
            }
            return orderDetails;
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsListAsync()
        {
            return await _serviceContext._OrderDetails.Where(o => o.IsDeleted != true).ToListAsync();
        }

        public Task<bool> UpdateOrderAsync(OrderDetails orderDetails, Guid orderId, Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
