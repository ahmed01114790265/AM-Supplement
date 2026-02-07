using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.AuditEntityInterfaces
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        Guid? CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedBy { get; set; }
    }

}
