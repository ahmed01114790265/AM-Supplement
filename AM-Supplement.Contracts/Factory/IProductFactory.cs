using AM_Supplement.Contracts.DTO;
using AMSupplement.Domain.Entities;
namespace AM_Supplement.Contracts.Factory
{
    public interface IProductFactory
    {
        public Product CreateProduct(ProductDTO productDTO);
        ProductDTO CreateProductDTO(Product product);
        public void UpdateProduct(Product product, ProductDTO productDTO);
        public bool ValidateBeforeDelete(Guid productId,Guid productDTOId);
    }
}
