using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;

namespace AM_Sopplement.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public Task<Product> GetProduct(Guid productid);
        public Task DeleteProduct(Product product);
        public Task<List<Product>> GetListOfProduct(int PageNumber, int PageSize, ProductType fillter, TypeSorting sorting);
    }
}
