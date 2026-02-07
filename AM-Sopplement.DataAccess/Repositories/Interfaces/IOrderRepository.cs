using AMSupplement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Sopplement.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetPendingOrder(Guid userId);
        Task Create(Order order);
        Task Update(Order order);
        Task<List<Order>> GetUserOrders(Guid userId);
        Task<Order?> GetOrderDetails(Guid orderId, Guid userId);

    }

}
