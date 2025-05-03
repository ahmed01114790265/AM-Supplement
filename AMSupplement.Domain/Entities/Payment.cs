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
        public int  Amountorders { get; set; }
        public string Paymentstaus {  get; set; }
        public Guid PaymentprocessId { get; set; }
        public Paymentprocess Paymentprocess { get; set; }
        public Guid OrdeId { get; set; }    
        public Order Order { get; set; }
    }
}
