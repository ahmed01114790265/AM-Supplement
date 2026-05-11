using AM_Supplement.Contracts.DTO;
using AM_Supplement.Shared.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AM_Supplement.Presentation.Models
{
    public class ProductsListViewModel
    {
        public List<ProductDTO> ModelList { get; set; }
        public int TotalPages { get; set; }
    }


}
