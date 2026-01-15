using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Resturant.Request
{
    public class UpdateRestaurantRequestDTO
    {
        [Required(ErrorMessage = "Restaurant ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Restaurant ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Restaurant name is required")]
        [StringLength(100, ErrorMessage = "Restaurant name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid City ID is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Cuisine type is required")]
        [StringLength(50, ErrorMessage = "Cuisine type cannot exceed 50 characters")]
        public string CuisineType { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public decimal Rating { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string PhoneNumber { get; set; }
    }
}
