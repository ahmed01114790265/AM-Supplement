using AM_Supplement.Contracts.DTO;
using AMSupplement.Domain.Entities;
namespace AM_Supplement.Contracts.Factory
{
    public interface IProductFactory
    {
        public Product CreateProduct(ProductDTO productDTO);
        public ProductDTO GetProductDTO(Product product);
        public void UpdateProduct(Product product, ProductDTO productDTO);
        public bool Validate_Before_Delete(Product product, ProductDTO productDTO);
    }
}
