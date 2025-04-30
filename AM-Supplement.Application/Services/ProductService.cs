using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Supplement.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
