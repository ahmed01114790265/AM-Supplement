using AM_Supplement.Shared.Enums;

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
