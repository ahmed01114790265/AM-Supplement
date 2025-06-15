using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AM_Supplement.Contracts.ResultModel;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Enums;
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
        public async Task<ResultList<ProductDTO>> GetListofProduct(int? PageNumber, int? PageSize,TypeSortingorFilltring sorting)
        {
            int PN = PageNumber.HasValue ? PageNumber.Value : 1;
            int PS = PageSize.HasValue ? PageSize.Value : 6;

            var ListProduct = await ProductRepository.GetListOfProduct(PN,PS);
      
           switch(sorting)
            {
                case TypeSortingorFilltring.Featured:
                    ListProduct = ListProduct.OrderBy(x=> x.Id).ToList();
                    break;
                case TypeSortingorFilltring.Bestselling:
                    ListProduct = ListProduct.OrderByDescending(x => x.Price)
                   .ToList();
                    break;
                 case TypeSortingorFilltring.AlphabeticalllyA_to_Z:
                    ListProduct = ListProduct.OrderBy(x =>x.Name)
                        .ToList();
                    break;
                case TypeSortingorFilltring.AlphabeticalllyZ_to_A:
                    ListProduct = ListProduct.OrderByDescending(x => x.Name)
                        .ToList();
                    break;
                case TypeSortingorFilltring.PriceLow_to_high:
                    ListProduct = ListProduct.OrderBy(x => x.Price)
                        .ToList();
                    break;
                case TypeSortingorFilltring.priceHigh_to_Low:
                    ListProduct = ListProduct.OrderByDescending(x => x.Price)
                        .ToList();
                    break;
                case TypeSortingorFilltring.ProductTypeAmino:
                    ListProduct = ListProduct.Where(x=>x.Type == ProductType.Amino)
                        .Order() .ToList();
                    break ;
                case TypeSortingorFilltring.ProductTypeGainer:
                    ListProduct = ListProduct.Where(x=> x.Type == ProductType.Gainer)
                        .OrderBy(x=> x.Type == ProductType.Gainer).ToList();
                    break ;
                case TypeSortingorFilltring.ProductTypeWheyProtein:
                     ListProduct = ListProduct.Where(x=> x.Type == ProductType.WheyProtein)
                        .OrderBy(x=> x.Type == ProductType.WheyProtein) .ToList();
                    break ;
                case TypeSortingorFilltring.ProductTypeCreatine:
                    ListProduct = ListProduct.Where(x => x.Type == ProductType.Creatine)
                        .OrderBy(x=> x.Type == ProductType.Creatine).ToList();
                    break;
                case TypeSortingorFilltring.ProductTypeCasien: 
                    ListProduct = ListProduct.Where(x=>x.Type == ProductType.casein)
                        .OrderBy(x => x.Type == ProductType.casein).ToList() ;
                    break ;
                case TypeSortingorFilltring.ProductTypePre_workout:
                    ListProduct = ListProduct.Where(x => x.Type == ProductType.Pre_workout)
                        .OrderBy(x => x.Type == ProductType.Pre_workout).ToList();
                    break;

            }
                     
            if(ListProduct==null || ListProduct.Count==0)
            {
                return new ResultList<ProductDTO>
                    {
                        IsVallid = false,
                        ErorrMassege = "list is emptey",

                    };
            }
            var ListProductDTO = ProductFactory.CreateListofProductDTO( ListProduct);
            return new ResultList<ProductDTO>
            {
                IsVallid = true,
                ModelList = ListProductDTO,
                TotalPages = ListProductDTO.Count/PS,
            };
                
        }
       
    }
}
