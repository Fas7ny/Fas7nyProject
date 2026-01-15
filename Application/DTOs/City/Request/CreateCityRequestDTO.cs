using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.City.Request
{
    public class CreateCityRequestDTO
    {
        [Required(ErrorMessage = "City name is required")]
        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Country ID is required")]
        public int CountryId { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
        public string ImageUrl { get; set; }
    }
}
