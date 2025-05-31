using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AMSupplement.Domain;

namespace AM_Sopplement.DataAccess.UnitOfWork.Implementation
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
