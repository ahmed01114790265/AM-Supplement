using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Sopplement.DataAccess.UnitOfWork.Interfaces
{
   public interface IUnitOfWork
    {
        public bool SaveChangs();
    }
}
