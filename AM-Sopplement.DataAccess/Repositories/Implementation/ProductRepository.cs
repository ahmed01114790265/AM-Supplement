using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Shared.Enums;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

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
        public async Task<List<Product>> GetProducts(int PageNumber,int PageSize , ProductType prodcutTypeFilte, TypeSorting sorting )
        {
            List<Product> productlist = new List<Product>();
            int SkippedPages = (PageNumber - 1) * PageSize;
            IQueryable<Product> products = AMSublementDbContext.Products
                  .Where(x => x.Type == prodcutTypeFilte);
            switch(sorting)
            {
                case TypeSorting.Featured:
                   products =  products.OrderBy(x => x.Id);
                    break;
                case TypeSorting.Bestselling:
                   products =  products.OrderByDescending(x => x.Price);
                    break;  
                case TypeSorting.AlphabeticalllyA_to_Z:
                    products = products.OrderBy(x => x.Name);
                    break;
                case TypeSorting.AlphabeticalllyZ_to_A:
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case TypeSorting.priceHigh_to_Low:
                    products = products.OrderBy(x => x.Price);
                    break;
                case TypeSorting.PriceLow_to_high:
                    products = products.OrderByDescending(x => x.Price);
                    break;
                default: 
                    products=products.OrderByDescending(x=>x.CreationDate);
                    break;
            }

            return await products.Skip(SkippedPages).Take(PageSize).ToListAsync();
          
        }
    }
}
