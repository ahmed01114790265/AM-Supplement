using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AMSupplement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
