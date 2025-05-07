using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Supplement.Contracts.Services;

namespace AM_Supplement.Application.Services
{
    class ProductService : IProductService
    {
        IProductRepository ProductRepository { get; set; }
        public ProductService(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
    }
}
