using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AMSupplement.Domain.Entities;


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
                Discount = productDTO.Discount,
                ImageUrl = productDTO.ImageUrl,
                Type = productDTO.Type,
                Weight = productDTO.Weight
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
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,
                Type = product.Type,
                Weight = product.Weight

            };
        }
        public void UpdateProduct(Product product, ProductDTO productDTO)
        {
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Taste = productDTO.Taste;
            product.Price = productDTO.Price;
            product.Discount = productDTO.Discount;
            product.ImageUrl = productDTO.ImageUrl;
            product.Type = productDTO.Type;
            product.Weight = productDTO.Weight;

        }
        public bool Validate_Before_Delete(Guid productId,Guid productDTOId)
        {
            return productId == productDTOId;
        }
      
    }
}
