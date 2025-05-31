using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AM_Sopplement.DataAccess.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        AMSublementDbContext AMSublementDbContext;
       IUnitOfWork UnitOfWork;
        public ProductRepository(AMSublementDbContext aMSublementDbContext, IUnitOfWork unitOfWork)
        {
            AMSublementDbContext = aMSublementDbContext;
            UnitOfWork = unitOfWork;
        }
        public Guid CreateProduct(Product product)
        {
            AMSublementDbContext.Products.Add(product);  
                return product.Id; 
        }

        public  async Task<Product> GetProduct(Guid productid)
        {
            var result = await AMSublementDbContext.Products.FirstOrDefaultAsync(x => x.Id == productid);
            return result == null ? new Product() : result;

        }
         public void DeleteProduct(Product product)
        {
            AMSublementDbContext.Products.Remove(product);
        }
    }
}
