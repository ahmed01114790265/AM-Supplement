using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Supplement.Infrastructure.Persistence;
using AM_Supplement.Shared.Enums;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Sopplement.DataAccess.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AMSublementDbContext _context;

        public OrderRepository(AMSublementDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetPendingOrder(Guid userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o =>
                    o.UserId == userId &&
                    o.Status == OrderStatus.Pending);
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
        public async Task Update(Order order)
        {
            _context.Orders.Update(order);
        }
        public async Task<List<Order>> GetUserOrders(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && o.Status != OrderStatus.Pending)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderDetails(Guid orderId, Guid userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o =>
                    o.Id == orderId &&
                    o.UserId == userId);
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }



    }

}
