using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Cart.Request
{
    public class GetCartRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
    }
}
