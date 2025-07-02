using AMSupplement.Domain.AuditEntityInterfaces;

namespace AMSupplement.Domain.Entities
{
    public  class Order : AuditableEntity
    { 
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }  
    }
}
