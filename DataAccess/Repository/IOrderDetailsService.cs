using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailsService
    {
        Task<IEnumerable<OrderDetails>> GetOrderDetailsListAsync();
        Task<OrderDetails?> GetOrderDetailsByIdAsync(Guid orderId, Guid productId);
        Task<bool> UpdateOrderAsync(OrderDetails orderDetails, Guid orderId, Guid productId);
        Task<bool> DeleteOrderDetailsAsync(Guid orderId, Guid productId);
        Task CreateOrderDetailsAsync(OrderDetails orderDetails);
    }
}
