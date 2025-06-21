using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;
using AMSupplement.Domain.Entities.CustomEntities;

namespace AM_Sopplement.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public Task<Product> GetProduct(Guid productid);
        public Task DeleteProduct(Product product);
        public Task<ProductListData> GetProducts(int PageNumber, int PageSize, ProductType? prodcutTypeFilter, TypeSorting? sorting);
    }
}
