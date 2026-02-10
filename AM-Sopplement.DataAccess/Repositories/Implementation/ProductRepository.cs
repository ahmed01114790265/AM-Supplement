using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Infrastructure.Persistence;
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
        private readonly AMSublementDbContext _context;

        public ProductRepository(AMSublementDbContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<(List<Product> Products, int TotalCount)> GetProducts(
      int pageNumber,
      int pageSize,
      ProductType? productTypeFilter,
      TypeSorting? sorting)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 6 : pageSize;

            int skipCount = (pageNumber - 1) * pageSize;

            IQueryable<Product> query = _context.Products
                .Where(x => x.IsActive)
                .Where(x => !productTypeFilter.HasValue || x.Type == productTypeFilter.Value);

            query = sorting switch
            {
                TypeSorting.Bestselling =>
                    query.OrderByDescending(x =>
                        x.OrderItems.Any()
                            ? x.OrderItems.Sum(o => o.Quantity)
                            : 0),

                TypeSorting.AlphabeticalllyA_to_Z =>
                    query.OrderBy(x => x.Name),

                TypeSorting.AlphabeticalllyZ_to_A =>
                    query.OrderByDescending(x => x.Name),

                TypeSorting.priceHigh_to_Low =>
                    query.OrderByDescending(x => x.Price),

                TypeSorting.PriceLow_to_high =>
                    query.OrderBy(x => x.Price),

                _ =>
                    query.OrderByDescending(x => x.CreatedDate)
                         .ThenBy(x => x.Id)
            };

            int totalCount = await query.CountAsync();

            var products = await query
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

     
    }
}
