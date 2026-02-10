using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Factory;
using AM_Supplement.Contracts.ResultModel;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Enums;
using AM_Supplement.Shared.Localization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Services
{
    public class ProductService : IProductService
    {
        readonly IProductRepository ProductRepository;
        readonly IProductFactory ProductFactory;
        readonly IUnitOfWork UnitOfWork;
        readonly IStringLocalizer<SharedResource> Localizer;
        readonly IImageService _imageService; // Corrected naming convention

        public ProductService(IProductRepository productRepository,
                              IProductFactory productFactory,
                              IUnitOfWork unitOfWork,
                              IStringLocalizer<SharedResource> localizer,
                              IImageService imageService)
        {
            ProductRepository = productRepository;
            ProductFactory = productFactory;
            UnitOfWork = unitOfWork;
            Localizer = localizer;
            _imageService = imageService;
        }

        public async Task<ResultModel<Guid>> AddProduct(ProductDTO productDTO)
        {
            // 1. Process Image if it exists
            // في AddProduct
            if (productDTO.ImageFile != null)
            {
                productDTO.ImageUrl = await _imageService.UploadImageAsync(productDTO.ImageFile, "products");
            }
            else
            {
                productDTO.ImageUrl = "default-product.png"; // تأكد من وجود صورة بهذا الاسم في مجلد products
            }
            var product = ProductFactory.CreateProduct(productDTO);
            ProductRepository.CreateProduct(product);
            await UnitOfWork.SaveChangsAsync();

            return new ResultModel<Guid>()
            {
                IsValid = true,
                Model = product.Id
            };
        }
        public async Task<ResultModel<Guid>> UpdateProduct(ProductDTO productDTO)
        {
            if (!productDTO.Id.HasValue)
                return new ResultModel<Guid> { IsValid = false, ErrorMessage = "Product ID is missing." };

            var product = await ProductRepository.GetProduct(productDTO.Id.Value);
            if (product == null)
                return new ResultModel<Guid> { IsValid = false, ErrorMessage = $"Product with Id {productDTO.Id} does not exist" };

            // المنطق المحدث لمعالجة الصورة
            if (productDTO.ImageFile != null)
            {
                // 1. حذف الصورة القديمة من المجلد الفيزيائي (نرسل اسم الملف والمجلد)
                _imageService.DeleteImage(product.ImageUrl, "products");

                // 2. رفع الصورة الجديدة وحفظ اسم الملف الجديد في الـ DTO
                productDTO.ImageUrl = await _imageService.UploadImageAsync(productDTO.ImageFile, "products");
            }
            else
            {
                // 3. الاحتفاظ باسم الصورة القديمة إذا لم يتم رفع ملف جديد
                productDTO.ImageUrl = product.ImageUrl;
            }

            // تحديث البيانات باستخدام الفاكتوري
            ProductFactory.UpdateProduct(product, productDTO);

            await UnitOfWork.SaveChangsAsync();

            return new ResultModel<Guid> { IsValid = true, Model = product.Id };
        }

        public async Task<ResultModel<bool>> DeleteProduct(Guid productId)
        {
            var product = await ProductRepository.GetProduct(productId);
            if (product == null)
                return new ResultModel<bool> { IsValid = false, ErrorMessage = $"Product with Id {productId} not found" };

            // تنظيف ملف الصورة من السيرفر قبل حذف السجل من قاعدة البيانات
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                _imageService.DeleteImage(product.ImageUrl, "products");
            }

            product.IsDeleted = true;
            await UnitOfWork.SaveChangsAsync();

            return new ResultModel<bool> { IsValid = true, Model = true };
        }

        public async Task<ResultModel<ProductDTO>> GetProductById(Guid productid)
        {
            var product = await ProductRepository.GetProduct(productid);
            if (product == null)
            {
                return new ResultModel<ProductDTO>
                {
                    IsValid = false,
                    ErrorMessage = Localizer["ProductNotFount"]
                };
            }
            var result = ProductFactory.CreateProductDTO(product);
            return new ResultModel<ProductDTO> { IsValid = true, Model = result };
        }

        public async Task<ResultList<ProductDTO>> GetProductsList(
            int? pageIndex = null,
            int? pageSize = null,
            ProductType? productTypeFilter = null,
            TypeSorting? sorting = null)
        {
            pageIndex ??= 1;
            pageSize ??= 6;

            var (productsList, totalCount) = await ProductRepository.GetProducts(
                pageIndex.Value,
                pageSize.Value,
                productTypeFilter,
                sorting
            );

            if (productsList == null || !productsList.Any())
            {
                return new ResultList<ProductDTO>
                {
                    IsValid = false,
                    ErrorMessage = Localizer["EmptyList"]
                };
            }

            var modelList = productsList
                .Select(ProductFactory.CreateProductDTO)
                .ToList();

            return new ResultList<ProductDTO>
            {
                IsValid = true,
                ModelList = modelList,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value)
            };
        }
    }
    }