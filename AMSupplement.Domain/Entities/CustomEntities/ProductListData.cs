using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities.CustomEntities
{
    public class ProductListData
    {
        public List<Product> Products { get; set; } 
        public int TotalCount { get; set; }
    }
}
