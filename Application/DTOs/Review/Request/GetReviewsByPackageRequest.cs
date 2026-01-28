using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Review.Request
{
    public class GetReviewsByPackageRequest
    {
        [Required(ErrorMessage = "Package ID is required")]
        public int PackageId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;

        [Range(1, 5, ErrorMessage = "Minimum rating must be between 1 and 5")]
        public int? MinRating { get; set; }
    }
}
