using AM_Supplement.Shared.Enums;

namespace AM_Supplement.Contracts.DTO
{
    public class ProductDTO
    {
        public DateTime CreationDate;

        public Guid? Id { get; set; }
        public string Name { get; set; } = null;
        public string Description { get; set; } = null;
        public decimal Price { get; set; }
        public string Taste { get; set; } = null;
        public string ImageUrl { get; set; }= "default-product.png";
        public decimal Weight { get; set; }
        public int Stock { get; set; }
        public int DiscountPercentage { get; set; }
        public ProductType Type { get; set; }
    }
}
