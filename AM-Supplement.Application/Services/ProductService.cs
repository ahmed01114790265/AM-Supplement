using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AM_Supplement.Contracts.ResultModel;
using AM_Supplement.Contracts.Services;

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
        public  ResultModel<Guid> AddProduct(ProductDTO productDTO)
        {
         var product =  ProductFactory.CreateProduct(productDTO);
         var productId = ProductRepository.CreateProduct(product);
          UnitOfWork.SaveChangs();
            if(productId == Guid.Empty)
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
        public async Task<ResultModel<ProductDTO>> Readonly_Product(Guid productid)
        {
           var product = await ProductRepository.GetProduct(productid);
            var result = ProductFactory.GetProductDTO(product);
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
        public async Task<ResultModel<Guid>> Update_Product(ProductDTO productDTO)
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
        public async Task Delete_Product(ProductDTO productDTO)
        {
            if (productDTO.Id == null)
                throw new ArgumentException("id is empty please enter product id");
          var product = await ProductRepository.GetProduct(productDTO.Id.Value);
            if (product == null)
                throw new ArgumentException("product not found please enter product");
            ProductFactory.Validate_Before_Delete(product,productDTO);
            ProductRepository.DeleteProduct(product);
            UnitOfWork.SaveChangs();    
        }
    }
}
