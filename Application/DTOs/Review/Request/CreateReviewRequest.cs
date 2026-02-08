using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Review.Request
{
    public class CreateReviewRequest
    {
        [Required(ErrorMessage = "Package ID is required")]
        [Range(1, int.MaxValue)]
        public int PackageId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(2000, MinimumLength = 10)]
        public string? Comment { get; set; }
    }

}
