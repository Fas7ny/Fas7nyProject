using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.CartItem.Request
{
    public class RemoveCartItemRequest
    {
        [Required(ErrorMessage = "Cart Item ID is required")]
        public Guid CartItemId { get; set; }

        [Required(ErrorMessage = "Cart ID is required")]
        public Guid CartId { get; set; }
    }
}
