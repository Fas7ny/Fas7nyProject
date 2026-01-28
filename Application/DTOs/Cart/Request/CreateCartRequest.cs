using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Cart.Request
{
    public class CreateCartRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string UserId { get; set; }
    }
}
