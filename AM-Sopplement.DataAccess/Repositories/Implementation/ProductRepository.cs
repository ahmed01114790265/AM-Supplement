using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AM_Sopplement.DataAccess.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
       readonly AMSublementDbContext AMSublementDbContext;
     
        public ProductRepository(AMSublementDbContext aMSublementDbContext)
        {
            AMSublementDbContext = aMSublementDbContext;
        }
        public void CreateProduct(Product product)
        {
            AMSublementDbContext.Products.Add(product);  
              
        }

        public  async Task<Product> GetProduct(Guid productid)
        {
            return await AMSublementDbContext.Products.FirstOrDefaultAsync(x => x.Id == productid);

        }
         public async Task DeleteProduct(Product product)
        {
            AMSublementDbContext.Products.Remove(product);
        }
        public async Task<List<Product>> GetListOfProduct()
        {
           return await AMSublementDbContext.Products.ToListAsync();
        }
    }
}
