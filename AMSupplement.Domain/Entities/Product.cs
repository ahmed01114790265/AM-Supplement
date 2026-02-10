using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.AuditEntityInterfaces;
namespace AMSupplement.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal Weight { get; set; }

        public string Taste { get; set; } = null!;

        public string ImageUrl { get; set; } = "default-product.png";

        public int DiscountPercentage { get; set; }  // 0 → 100
        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        public ProductType Type { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
