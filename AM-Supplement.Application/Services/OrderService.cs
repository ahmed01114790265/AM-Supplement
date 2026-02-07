using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderFactory _orderFactory;

        public OrderService(
            IOrderRepository orderRepo,
            IProductRepository productRepo,
            IUnitOfWork unitOfWork,
            IOrderFactory orderFactory)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _orderFactory = orderFactory;
        }

        public async Task AddToCart(Guid userId, Guid productId, int quantity)
        {
            var order = await _orderRepo.GetPendingOrder(userId);
            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    Status = OrderStatus.Pending
                };
                await _orderRepo.Create(order);
            }

            var item = order.OrderItems.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                var product = await _productRepo.GetProduct(productId);
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _unitOfWork.SaveChangsAsync();
        }

        public async Task<CartDTO> GetCart(Guid userId)
        {
            var order = await _orderRepo.GetPendingOrder(userId);

            if (order == null)
            {
                return new CartDTO
                {
                    Items = new List<CartItemDTO>()
                };
            }

            return _orderFactory.CreateCartDTO(order);
        }

        public async Task RemoveFromCart(Guid userId, Guid productId)
        {
            var order = await _orderRepo.GetPendingOrder(userId);
            if (order == null) return;

            var item = order.OrderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                order.OrderItems.Remove(item);

            await _unitOfWork.SaveChangsAsync();
        }

        public async Task UpdateQuantity(Guid userId, Guid productId, int quantity)
        {
            if (quantity <= 0) return;
            var order = await _orderRepo.GetPendingOrder(userId);
            if (order == null) return;

            var item = order.OrderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                await _unitOfWork.SaveChangsAsync();
            }
        }

        public async Task CompleteOrder(Guid userId)
        {
            var order = await _orderRepo.GetPendingOrder(userId);
            if (order == null) return;

            order.Status = OrderStatus.Paid;
            await _unitOfWork.SaveChangsAsync();
        }
        public async Task<List<OrderHistoryDTO>> GetOrderHistory(Guid userId)
        {
            var orders = await _orderRepo.GetUserOrders(userId);

            return orders.Select(o => new OrderHistoryDTO
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status
            }).ToList();
        }

        public async Task<OrderDetailsDTO?> GetOrderDetails(Guid orderId, Guid userId)
        {
            var order = await _orderRepo.GetOrderDetails(orderId, userId);
            if (order == null) return null;

            return new OrderDetailsDTO
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(i => new OrderItemDetailsDTO
                {
                    ProductName = i.Product.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Price = i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }


    }
}
