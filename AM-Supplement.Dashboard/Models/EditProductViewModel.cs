namespace AM_Supplement.Dashboard.Models
{
    using AM_Supplement.Shared.Enums;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class EditProductViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string Taste { get; set; } = string.Empty;

        public decimal Weight { get; set; }

        public int Stock { get; set; }

        public int DiscountPercentage { get; set; }

        [Required]
        public ProductType Type { get; set; }

        // الصورة الحالية
        public string CurrentImage { get; set; } = string.Empty;

        // صورة جديدة (اختياري)
        public IFormFile? NewImage { get; set; }
    }

}
