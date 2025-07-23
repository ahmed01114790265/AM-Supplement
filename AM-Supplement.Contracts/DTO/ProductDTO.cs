using AM_Supplement.Shared.Enums;
using AM_Supplement.Shared.Localization;
using System.ComponentModel.DataAnnotations;

namespace AM_Supplement.Contracts.DTO
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage ="ProductNameRequired")]
         [StringLength(100, ErrorMessage = "ProductNameMaxLength")]

        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "DescriptionMaxLength")]
        public string Description { get; set; }

        [Required(ErrorMessage = "PriceRequired")]
        [Range(0.01, 1000000, ErrorMessage = "PriceRange")]
        public double Price { get; set; }

        [StringLength(50, ErrorMessage = "TasteMaxLength")]
        public string Taste { get; set; }

        [Url(ErrorMessage = "ImageUrlInvalid")]
        public string ImageUrl { get; set; }

        [Range(0.01, 10000,ErrorMessage = "WeightRange")]
        public double Weight { get; set; }

        [Range(0, 100, ErrorMessage = "DiscountRange")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "TypeRequired")]
        public ProductType Type { get; set; }

        public DateTime? CreationDate { get; set; }

    }
}
