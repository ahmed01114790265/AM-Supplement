using AMSupplement.Domain.Entities;

namespace AM_Sopplement.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Guid Create_Product(Product product);
        public Product Get_Product(Guid productid);
        public void Update_Product_toSavechings();
        public void Delete_Product(Product product);
    }
}
