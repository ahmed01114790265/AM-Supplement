using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AMSupplement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Sopplement.DataAccess.UnitOfWork.Immpelementaion
{
   public class UnitOfWork : IUnitOfWork
    {
        AMSublementDbContext AMSublementDbContext;

      public UnitOfWork(AMSublementDbContext aMSublementDbContext)
        {
            AMSublementDbContext = aMSublementDbContext;
        }
        public bool SaveChangs()
        {
            try
            {
                AMSublementDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
