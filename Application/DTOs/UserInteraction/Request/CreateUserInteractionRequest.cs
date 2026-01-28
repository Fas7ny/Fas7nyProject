using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserInteraction.Request
{
    public class CreateUserInteractionRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Item type is required")]
        [StringLength(50, ErrorMessage = "Item type cannot exceed 50 characters")]
        [RegularExpression("^(TouristPlace|Hotel|Package|Restaurant|City|HotelRoom)$",
            ErrorMessage = "Item type must be: TouristPlace, Hotel, Package, Restaurant, City, or HotelRoom")]
        public string ItemType { get; set; }

        [Required(ErrorMessage = "Item ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Item ID must be a positive number")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Interaction type is required")]
        [StringLength(50, ErrorMessage = "Interaction type cannot exceed 50 characters")]
        [RegularExpression("^(View|Click|Favorite|Unfavorite|Share|Book|Search|Compare|AddToCart|RemoveFromCart)$",
            ErrorMessage = "Invalid interaction type")]
        public string InteractionType { get; set; }
    }
}
