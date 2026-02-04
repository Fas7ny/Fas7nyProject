using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Cart.Request
{
    public class ClearCartRequest
    {
        [Required(ErrorMessage = "Cart ID is required")]
        public int CartId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
    }
}
