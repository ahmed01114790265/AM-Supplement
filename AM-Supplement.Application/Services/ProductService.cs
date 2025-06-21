using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AM_Supplement.Contracts.ResultModel;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Services
{
    public class ProductService : IProductService
    {
        readonly IProductRepository ProductRepository;
        readonly IProductFactory ProductFactory;
        readonly  IUnitOfWork UnitOfWork;
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
            
            await ProductRepository.DeleteProduct(product);
            
            await  UnitOfWork.SaveChangsAsync();

            return new ResultModel<bool>
            {
                IsVallid = true,
                Model = true
            };
        }
        public async Task<ResultList<ProductDTO>> GetProductsList(int? pageIndex, int? pageSize, ProductType prodcutTypeFilter, TypeSorting? sorting)
        {
            pageIndex = pageIndex.HasValue ?  pageIndex.Value : 1;
            pageSize = pageSize.HasValue ? pageSize.Value : 6;

            var productsList = await ProductRepository.GetProducts(pageIndex.Value, pageSize.Value, prodcutTypeFilter, sorting);

            if (productsList == null || productsList.Products == null)
            {
                return new ResultList<ProductDTO>
                {
                    IsVallid = false,
                    ErorrMassege = " list is null "
                };
            }
            var modelList = productsList.Products.Select(x => ProductFactory.CreateProductDTO(x)).ToList();

            return new ResultList<ProductDTO>
            {
                IsVallid = true,
                ModelList = modelList,
                TotalPages = (int)Math.Ceiling((double)productsList.TotalCount / pageSize.Value),
            };
        }

    }
}
