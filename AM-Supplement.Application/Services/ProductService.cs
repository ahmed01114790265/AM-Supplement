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
         var product =  ProductFactory.CreateProduct(productDTO);
          ProductRepository.CreateProduct(product);
          UnitOfWork.SaveChangs();
         var  productId = await ProductRepository.GetProduct(product.Id);
            if(productId == null)
                return new ResultModel<Guid>()
                {
                    IsVallid = false,
                    ErorrMassege = "error whil saving product please try again",
                    Model = Guid.Empty
                };
            return new ResultModel<Guid>()
            {
                IsVallid = true,
                Model = productId
            };

        }
        public async Task<ResultModel<ProductDTO>> GetProductById(Guid productid)
        {
           var product = await ProductRepository.GetProduct(productid);
            var result = ProductFactory.CreateProductDTO(product);
            if (product == null)
                return new ResultModel<ProductDTO>()
                {
                    IsVallid = false,
                    ErorrMassege = "this product is not vallid or empty",
                    Model = new ProductDTO()
                };
            return new ResultModel<ProductDTO>()
            {
                IsVallid = true,
                Model = result
            };
        }
        public async Task<ResultModel<Guid>> UpdateProduct(ProductDTO productDTO)
        {
            if (productDTO.Id == null)
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
                    ErorrMassege = "this product is not found please try again"
                };
           ProductFactory.UpdateProduct(product, productDTO);
          UnitOfWork.SaveChangs();
            return new ResultModel<Guid>()
            {
                IsVallid = true,
                Model = product.Id
            };
        }
        public async Task DeleteProduct(Guid? productId)
        {
            if (productId == null)
                throw new ArgumentException("id is empty please enter product id");
          var product = await ProductRepository.GetProduct(productId.Value);
            if (product == null)
                throw new ArgumentException("product not found please enter product");
            ProductFactory.Validate_Before_Delete(product,productId.Value);
            ProductRepository.DeleteProduct(product);
            UnitOfWork.SaveChangs();    
        }
    }
}
