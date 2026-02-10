using AM_Supplement.Contracts.DTO;

namespace AM_Supplement.Dashboard.Models
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        public string UserEmail { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
