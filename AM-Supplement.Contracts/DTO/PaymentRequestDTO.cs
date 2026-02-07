using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.DTO
{
    public class PaymentRequestDTO
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // Mock | Stripe
    }

    public class PaymentResultDTO
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; }
    }

}
