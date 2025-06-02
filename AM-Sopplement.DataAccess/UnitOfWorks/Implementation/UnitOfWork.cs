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
            catch 
            {
                throw new ArgumentException("data do not save in database");
            }
        }

        public async Task <bool> SaveChangsAsync()
        {
            try
            {
              await  AMSublementDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new ArgumentException("data do not save in database");
            }
        }
    }
}
