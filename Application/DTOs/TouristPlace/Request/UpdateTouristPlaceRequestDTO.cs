using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.TouristPlace.Request
{
    public class UpdateTouristPlaceRequestDTO
    {
        [Required(ErrorMessage = "Tourist place ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Tourist Place ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tourist place name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid City ID is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Entry fee is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Entry fee must be greater than or equal to 0")]
        public decimal EntryFee { get; set; }

        [Required(ErrorMessage = "Opening hours is required")]
        [StringLength(100, ErrorMessage = "Opening hours cannot exceed 100 characters")]
        public string OpeningHours { get; set; }
    }
}
