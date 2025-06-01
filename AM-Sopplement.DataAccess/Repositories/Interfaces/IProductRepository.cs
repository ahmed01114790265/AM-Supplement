using AMSupplement.Domain.Entities;

namespace AM_Sopplement.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public Task<Product> GetProduct(Guid productid);
        public void DeleteProduct(Product product);
    }
}
