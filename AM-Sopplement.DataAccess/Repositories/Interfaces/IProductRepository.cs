using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.Entities;
using AMSupplement.Domain.Entities.CustomEntities;

namespace AM_Sopplement.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public Task<Product> GetProduct(Guid productid);
        public void DeleteProduct(Product product);
        public Task<(List<Product> Products, int TotalCount)> GetProducts(
      int pageNumber,
      int pageSize,
      ProductType? productTypeFilter,
      TypeSorting? sorting);
        void Restore(Product entity);
        Task<List<Product>> GetDeletedProductsAsync();
        Task<Product> GetProductForRestoreAsync(Guid id);
       public Task<int> TotalCountProduct();
    }
}
