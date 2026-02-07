using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AMSupplement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Factory
{
    public class OrderFactory  : IOrderFactory
    {
        public  CartDTO CreateCartDTO(Order order)
        {
            return new CartDTO
            {
                Items = order.OrderItems.Select(i => new CartItemDTO
                {
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Price = i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
 }
