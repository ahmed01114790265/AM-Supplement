using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; } 
        public virtual Product Product { get; set; }    
    }
}
