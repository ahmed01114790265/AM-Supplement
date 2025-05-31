using AM_Supplement.Shared.Enums;

namespace AM_Supplement.Contracts.DTO
{
    public class ProductDTO
    {
        
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Taste { get; set; }
        public string ImageUrl { get; set; }
        public double Weight { get; set; }
        public int Discount { get; set; }
        public ProductType Type { get; set; }
    }
}
