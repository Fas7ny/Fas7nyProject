using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Activity.Request
{
    public class CreateActivityRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 200 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(100, 10000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "CityId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "CityId must be a valid positive number")]
        public int CityId { get; set; }
        public string ImageUrl { get; set; }

    }
}
