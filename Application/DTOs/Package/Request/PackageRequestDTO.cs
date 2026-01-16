using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Package.Request
{
    public class PackageRequestDTO
    {
        [Required(ErrorMessage = "Package name is required")]
        [StringLength(100, ErrorMessage = "Package name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Destination ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Destination ID is required")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
