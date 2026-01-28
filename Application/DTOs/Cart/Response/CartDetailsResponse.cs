using Fas7ny.Application.DTOs.CartItem.Response;

namespace Fas7ny.Application.DTOs.Cart.Response
{
    public class CartDetailsResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<CartItemDetailsResponse> Items { get; set; } = new List<CartItemDetailsResponse>();
        public int TotalItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
