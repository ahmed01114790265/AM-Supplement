using AM_Supplement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.DTO.AdminDasboard
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } // Pending, Completed, Cancelled
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
