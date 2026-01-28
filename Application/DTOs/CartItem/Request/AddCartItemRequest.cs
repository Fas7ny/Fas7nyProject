using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.CartItem.Request
{
    public class AddCartItemRequest
    {
        [Required(ErrorMessage = "Cart ID is required")]
        public Guid CartId { get; set; }

        [Required(ErrorMessage = "Booking ID is required")]
        public Guid BookingId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
        public decimal Price { get; set; }
    }

}
