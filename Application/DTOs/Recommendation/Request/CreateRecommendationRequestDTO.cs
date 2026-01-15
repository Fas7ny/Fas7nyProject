using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Recommendation.Request
{
    public class CreateRecommendationDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid User ID is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Destination ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Destination ID is required")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Recommendation type is required")]
        [StringLength(50, ErrorMessage = "Recommendation type cannot exceed 50 characters")]
        public string RecommendationType { get; set; }

        [Required(ErrorMessage = "Score is required")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public decimal Score { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string Reason { get; set; }
    }
}
