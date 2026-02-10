using AM_Supplement.Contracts.DTO;

namespace AM_Supplement.Dashboard.Models
{
   
        public class ProductsListViewModel
        {
            public List<ProductDTO> Products { get; set; } = new();
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }

        }
   
}
