using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.DTO
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

        public decimal Total => Items.Sum(i => i.Total); // مجموع الكارت كله

        public bool IsEmpty => !Items.Any(); // للتحقق إذا الكارت فاضي
    }

}
