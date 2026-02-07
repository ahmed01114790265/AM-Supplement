using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.AuditEntityInterfaces;

namespace AMSupplement.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public Guid Id { get; set; }

        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        public PaymentStatus PaymentStatus { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }

}
