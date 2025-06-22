using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Shared.Enums;
using AMSupplement.Domain;
using AMSupplement.Domain.Entities;
using AMSupplement.Domain.Entities.CustomEntities;
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
        public async Task<ProductListData> GetProducts(int PageNumber,int PageSize , ProductType? prodcutTypeFilter, TypeSorting? sorting)
        {
            int SkippedPages = (PageNumber - 1) * PageSize;
            IQueryable<Product> products = AMSublementDbContext.Products
                  .Where(x => !prodcutTypeFilter.HasValue || x.Type == prodcutTypeFilter.Value);

            switch(sorting)
            {
                // best sellers need to be ordered by  more selling products
                case TypeSorting.Bestselling:
                   products =  products.OrderByDescending(x => x.OrderItems.Sum(x=>x.Quantity));
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
            int count = await products.CountAsync();

            var productsList = await products.Skip(SkippedPages).Take(PageSize).ToListAsync();
            return new ProductListData
            {
                Products = productsList,
                TotalCount = count
            };
        }
    }
}
