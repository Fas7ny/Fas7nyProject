using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.CartItem.Request
{
    public class UpdateCartItemRequest
    {
        [Required(ErrorMessage = "Cart Item ID is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }
    }
}
