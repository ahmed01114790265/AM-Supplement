using AM_Supplement.Contracts.DTO;

namespace AM_Supplement.Dashboard.Models
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public List<CartItemDTO> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

}
