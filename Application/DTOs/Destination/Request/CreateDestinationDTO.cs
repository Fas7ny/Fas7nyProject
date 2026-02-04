using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Destination.Request
{
    public class CreateDestinationDTO
    {
        [Required(ErrorMessage = "Destination name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "City ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid City ID is required")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }
        [Url(ErrorMessage = "Invalid URL format")]
        [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
        public string ImageUrl { get; set; }
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public decimal Rating { get; set; }
    }
}
