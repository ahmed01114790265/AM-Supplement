using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.ResultModel;
using AM_Supplement.Shared.Enums;
using System.Runtime.CompilerServices;

namespace AM_Supplement.Contracts.Services
{
    public interface IProductService
    {
        public Task<ResultModel<Guid>> AddProduct(ProductDTO productDTO);
        public Task<ResultModel<ProductDTO>> GetProductById(Guid productid);
        public Task<ResultModel<Guid>> UpdateProduct(ProductDTO productDTO);
        Task<ResultModel<bool>> DeleteProduct(Guid productId);
        public Task<ResultList<ProductDTO>> GetProductsList(int? pageIndex, int? pageSize, ProductType prodcutTypeFilte, TypeSorting? sorting);
    }
}
