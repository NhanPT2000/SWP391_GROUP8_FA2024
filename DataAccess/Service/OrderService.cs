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
    public class OrderService : IOrderService
    {
        private readonly PetShopContext _serviceContext;
        public OrderService(PetShopContext dbContext)
        {
            _serviceContext = dbContext;
        }
        public async Task CreateOrderAsync(Order order)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            try
            {
                var newOrder = new Order
                {
                    OrderId = order.OrderId,
                    Freight = order.Freight,
                    Member = order.Member,
                    MemberId = order.MemberId,
                    OrderDate = DateTime.UtcNow,
                    RequiredDate = DateTime.UtcNow.AddDays(7),
                    ShippedDate = DateTime.UtcNow
                };

                await _serviceContext.Orders.AddAsync(newOrder);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var orderToDelete = await _serviceContext.Orders.Include(o => o.Member).FirstOrDefaultAsync(p => p.OrderId == id);

            if (orderToDelete == null)
            {
                return false;
            }

            if (orderToDelete.Member != null) orderToDelete.IsDeleted = true;
            else _serviceContext.Remove(orderToDelete);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            var order = await _serviceContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return default;
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Guid userId)
        {
            var orders = from order in _serviceContext.Orders
                         where order.MemberId == userId
                         select order;

            return await orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.IsDeleted != true)
                .Where(o => o.IsPaid != true)
                .ToListAsync();
        }

        public Task<bool> UpdateOrderAsync(Order order, Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<Order> GetOrCreateOrderAsync(Guid userId)
        {
            var order = await _serviceContext.Orders
                .FirstOrDefaultAsync(o => o.MemberId == userId && o.ShippedDate == null);

            if (order == null)
            {
                order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    MemberId = userId,
                    OrderDate = DateTime.UtcNow,
                    RequiredDate = DateTime.UtcNow.AddDays(7),
                    ShippedDate = null
                };
                await _serviceContext.Orders.AddAsync(order);
                await _serviceContext.SaveChangesAsync();
            }

            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrdersByIdAsync(Guid[] orderIds)
        {
            if (orderIds == null || orderIds.Length == 0)
            {
                return null;
            }

            var orders = await _serviceContext.Orders
                .Where(order => orderIds.Contains(order.OrderId))
                .Include(order => order.OrderDetails)
                .ToListAsync();

            return orders;
        }

        public async Task<bool> PaidOrder(Guid id)
        {
           var paid = await _serviceContext.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.MemberId == id)
                .ToListAsync();
            foreach ( var o in paid) 
            {
               o.IsPaid = true;
                _serviceContext.Orders.Update(o);
            }
            return await _serviceContext.SaveChangesAsync() > 0;
        }
    }
}
