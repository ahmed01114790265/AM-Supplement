using AM_Sopplement.DataAccess.Repositories.Interfaces;
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
        public ProductService(IProductRepository productRepository, IProductFactory productFactory)
        {
            ProductRepository = productRepository;
            ProductFactory = productFactory;
        }
       public  ResultModel<Guid> AddProduct(ProductDTO productDTO)
        {
         var product =  ProductFactory.Create_Product(productDTO);
         var productId = ProductRepository.Create_Product(product);
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
        public ResultModel<ProductDTO> Readonly_Product(Guid productid)
        {
           var product = ProductRepository.Get_Product(productid);
            var result = ProductFactory.Get_Product(product);
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
        public ResultModel<Guid> Update_Product(ProductDTO productDTO)
        {
            if (productDTO.Id == null)
                return new ResultModel<Guid>()
                {
                    IsVallid = false,
                    ErorrMassege = "productid is empty please enter date"
                };

         var product =   ProductRepository.Get_Product(productDTO.Id.Value);
            if (product == null)
                return new ResultModel<Guid>()
                {
                    IsVallid = false,
                    ErorrMassege = "this product is not found please try again"
                };
           ProductFactory.Update_Product(product, productDTO);
            ProductRepository.Update_Product_toSavechings();
            return new ResultModel<Guid>()
            {
                IsVallid = true,
                Model = product.Id
            };
        }
        public void Delete_Product(ProductDTO productDTO)
        {
            if (productDTO.Id == null)
                throw new ArgumentException("id is empty please enter product id");
          var product =  ProductRepository.Get_Product(productDTO.Id.Value);
            if (product == null)
                throw new ArgumentException("product not found please enter product");
            ProductFactory.Validate_Before_Delete(product,productDTO);
            ProductRepository.Delete_Product(product);
        }
    }
}
