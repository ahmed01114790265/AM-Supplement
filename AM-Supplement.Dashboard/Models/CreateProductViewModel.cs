namespace AM_Supplement.Dashboard.Models
{
    using AM_Supplement.Shared.Enums;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Taste { get; set; }

        public decimal Weight { get; set; }

        public int Stock { get; set; }

        public int DiscountPercentage { get; set; }

        [Required]
        public ProductType Type { get; set; }

        [Display(Name = "Product Image")]
       
        public IFormFile Image { get; set; }
    }

}
