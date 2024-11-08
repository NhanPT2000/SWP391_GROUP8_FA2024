using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(Guid userId);
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<bool> UpdateOrderAsync(Order order, Guid id);
        Task<bool> DeleteOrderAsync(Guid id);
        Task CreateOrderAsync(Order order);
        Task<Order> GetOrCreateOrderAsync(Guid userId);
        Task<IEnumerable<Order>?> GetOrdersByIdAsync(Guid[] orderIds);

        Task<bool> PaidOrder(Guid id);
    }
}
