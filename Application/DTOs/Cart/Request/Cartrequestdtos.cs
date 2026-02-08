using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Cart.Request
{


    public class AddCartItemRequest
    {
        [Required(ErrorMessage = "Booking ID is required")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; } = 1;
    }
    public class UpdateCartItemQuantityRequest
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }
    }
}
