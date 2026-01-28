using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Review.Request
{
    public class CreateReviewRequest
    {
        [Required(ErrorMessage = "Package ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Package ID must be a positive number")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars")]
        public int Rating { get; set; }

        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Comment must be between 10 and 2000 characters")]
        public string? Comment { get; set; }
    }
}
