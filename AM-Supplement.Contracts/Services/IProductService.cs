using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.ResultModel;

namespace AM_Supplement.Contracts.Services
{
    public interface IProductService
    {
        public Task <ResultModel<Guid>> AddProduct(ProductDTO productDTO);
        public Task<ResultModel<ProductDTO>> Readonly_Product(Guid productid);
        public Task<ResultModel<Guid>> Update_Product(ProductDTO productDTO);
        public  Task Delete_Product(ProductDTO productDTO);
    }
}
