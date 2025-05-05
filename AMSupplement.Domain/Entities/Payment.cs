using AM_Supplement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities
{
   public class Payment
    {  
        public Guid Id { get; set; }
        public DateTime Paydate { get; set; } = DateTime.Now;
        public PaymentStatus Paymentstaus {  get; set; }
        public string PaymentMethod { get; set; }
        public Guid OrderId { get; set; }    
        public Order Order { get; set; }
    }
}
