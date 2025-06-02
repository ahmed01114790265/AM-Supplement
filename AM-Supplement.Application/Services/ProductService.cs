using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AM_Supplement.Contracts.ResultModel;
using AM_Supplement.Contracts.Services;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Services
{
    public class ProductService : IProductService
    {
        IProductRepository ProductRepository { get; set; }
        IProductFactory ProductFactory;
        IUnitOfWork UnitOfWork;
        public ProductService(IProductRepository productRepository, IProductFactory productFactory, IUnitOfWork unitOfWork)
        {
            ProductRepository = productRepository;
            ProductFactory = productFactory;
            UnitOfWork = unitOfWork;
        }
        public  async Task<ResultModel<Guid>> AddProduct(ProductDTO productDTO)
        {
            var product = ProductFactory.CreateProduct(productDTO);
            ProductRepository.CreateProduct(product);
            await UnitOfWork.SaveChangsAsync();

            return new ResultModel<Guid>()
            {
                IsVallid = true,
                Model = product.Id
            };

        }
        public async Task<ResultModel<ProductDTO>> GetProductById(Guid productid)
        {
            var product = await ProductRepository.GetProduct(productid);
            if(product == null)
            {
                return new ResultModel<ProductDTO>
                {
                    IsVallid = false,
                    ErorrMassege = "product is not found"
                };
            }
            var result = ProductFactory.CreateProductDTO(product);
            return new ResultModel<ProductDTO>()
            {
                IsVallid = true,
                Model = result
            };
        }
        public async Task<ResultModel<Guid>> UpdateProduct(ProductDTO productDTO)
        {
            if (!productDTO.Id.HasValue)
                return new ResultModel<Guid>()
                {
                    IsVallid = false,
                    ErorrMassege = "productid is empty please enter date"
                };

               var product = await  ProductRepository.GetProduct(productDTO.Id.Value);
            if (product == null)
                return new ResultModel<Guid>()
                {
                    IsVallid = false,
                    ErorrMassege = $"product with Id {productDTO.Id} is not exists"
                };

                ProductFactory.UpdateProduct(product, productDTO);
                await UnitOfWork.SaveChangsAsync();
                return new ResultModel<Guid>()
                {
                    IsVallid = true,
                    Model = product.Id
                };
        }
        public async Task<ResultModel<bool>> DeleteProduct(Guid productId)
        {
            var product = await ProductRepository.GetProduct(productId);

            if (product == null)
                return new ResultModel<bool>
                {
                    IsVallid = false,
                    ErorrMassege = $"product with Id {productId} not found"
                };
            
            ProductRepository.DeleteProduct(product);
            
            await  UnitOfWork.SaveChangsAsync();

            return new ResultModel<bool>
            {
                IsVallid = true,
                Model = true
            };
        }
    }
}
