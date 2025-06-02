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
            AMSublementDbContext.SaveChanges();
            return true;
        }

        public async Task <bool> SaveChangsAsync()
        {
            await AMSublementDbContext.SaveChangesAsync();
            return true;
        }
    }
}
