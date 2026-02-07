using AM_Supplement.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.Services
{
    public interface IOrderService
    {
        Task AddToCart(Guid userId, Guid productId, int quantity);
        Task<CartDTO> GetCart(Guid userId);
        Task RemoveFromCart(Guid userId, Guid productId);
        Task UpdateQuantity(Guid userId, Guid productId, int quantity);
        Task CompleteOrder(Guid userId);
        Task<List<OrderHistoryDTO>> GetOrderHistory(Guid userId);
        Task<OrderDetailsDTO?> GetOrderDetails(Guid orderId, Guid userId);

    }

}
