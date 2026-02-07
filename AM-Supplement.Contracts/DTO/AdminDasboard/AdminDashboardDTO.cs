using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.DTO.AdminDasboard
{
    public class AdminDashboardDTO
    {
        public IEnumerable<OrderDTO> Orders { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }

}
