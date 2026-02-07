using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities
{
    public class ApplicationUser  : IdentityUser<Guid>
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}
