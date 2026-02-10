using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AMSupplement.Domain.Entities;
using AMSupplement.Domain.Entities.CustomEntities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace AM_Supplement.Application.Factory
{
 public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductDTO productDTO)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = productDTO.Name,
                Description = productDTO.Description,
                Taste = productDTO.Taste,
                Price = productDTO.Price,
                DiscountPercentage = productDTO.DiscountPercentage,
                ImageUrl = string.IsNullOrEmpty(productDTO.ImageUrl) ? "default-product.png" : productDTO.ImageUrl,
                Type = productDTO.Type,
                Weight = productDTO.Weight,
                Stock = productDTO.Stock
            };
        }
        public ProductDTO CreateProductDTO(Product product)
        {
            return new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Taste = product.Taste,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                ImageUrl = product.ImageUrl,
                Type = product.Type,
                Weight = product.Weight,
                CreationDate = product.CreatedDate,
                Stock = product.Stock
            };
        }
        public void UpdateProduct(Product product, ProductDTO productDTO)
        {
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Taste = productDTO.Taste;
            product.Price = productDTO.Price;
            product.DiscountPercentage = productDTO.DiscountPercentage;
            product.Type = productDTO.Type;
            product.Weight = productDTO.Weight;
            product.Stock = productDTO.Stock;

            // تحديث الصورة فقط إذا تم رفع صورة جديدة
            if (!string.IsNullOrEmpty(productDTO.ImageUrl))
            {
                product.ImageUrl = productDTO.ImageUrl;
            }
        }
        public bool ValidateBeforeDelete(Guid productId,Guid productDTOId)
        {
            if (productId == productDTOId)
                return true;
            return false;
        }

    }
}
