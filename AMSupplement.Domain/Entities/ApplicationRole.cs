using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base() { }

        // Use 'name' instead of 'roleName' to satisfy EF Core binding
        public ApplicationRole(string name) : base(name)
        {
        }
    }
}
