using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.AuditEntityInterfaces
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        protected AuditableEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }

}
