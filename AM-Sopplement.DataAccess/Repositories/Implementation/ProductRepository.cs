using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AMSupplement.Domain;

namespace AM_Sopplement.DataAccess.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        AMSublementDbContext AMSublementDbContext { get; set; }
        public ProductRepository(AMSublementDbContext aMSublementDbContext)
        {
            AMSublementDbContext = aMSublementDbContext; 
        }

    }
}
