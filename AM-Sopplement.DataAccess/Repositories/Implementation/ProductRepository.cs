using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;

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
        public Guid Create_Product(Product product)
        {
            AMSublementDbContext.Products.Add(product);
            UnitOfWork.SaveChangs();
            var result = AMSublementDbContext.Products.FirstOrDefault(x=> x.Id == product.Id);  
            if(result == null)
                return Guid.Empty;
                return result.Id; 
        }

        public Product Get_Product(Guid productid)
        {
          var result =  AMSublementDbContext.Products.FirstOrDefault(x => x.Id == productid);
            return result == null ? new Product() : result;
        }
        public void Update_Product_toSavechings()
        {
            UnitOfWork.SaveChangs();
        }
         public void Delete_Product(Product product)
        {
            AMSublementDbContext.Products.Remove(product);
            UnitOfWork.SaveChangs();
        }
    }
}
