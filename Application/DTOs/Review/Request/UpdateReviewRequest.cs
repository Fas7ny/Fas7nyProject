using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Review.Request
{
    public class UpdateReviewRequest
    {
        [Required(ErrorMessage = "Review ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Review ID must be a positive number")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars")]
        public int Rating { get; set; }

        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Comment must be between 10 and 2000 characters")]
        public string? Comment { get; set; }
    }
}
