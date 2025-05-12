using AM_Supplement.Contracts.DTO;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;
namespace AM_Supplement.Contracts.Factory
{
    public interface IProductFactory
    {
        public  Product Create_Product(ProductDTO productDTO);
        public ProductDTO Get_Product(Product product);
        public void Update_Product(Product product, ProductDTO productDTO);
        public bool Validate_Before_Delete(Product product, ProductDTO productDTO);
    }
}
