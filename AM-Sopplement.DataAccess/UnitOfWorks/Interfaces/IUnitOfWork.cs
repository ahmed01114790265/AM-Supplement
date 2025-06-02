namespace AM_Sopplement.DataAccess.UnitOfWork.Interfaces
{
   public interface IUnitOfWork
    {
        public bool SaveChangs();
        public Task<bool> SaveChangsAsync();
    }
}
