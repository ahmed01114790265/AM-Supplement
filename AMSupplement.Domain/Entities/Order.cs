using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.AuditEntityInterfaces;

namespace AMSupplement.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

}
