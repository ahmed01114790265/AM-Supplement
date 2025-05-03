using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities
{
   public  class Paymentprocess
    {
        public Guid Id { get; set; }
        public string ProcessName { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
