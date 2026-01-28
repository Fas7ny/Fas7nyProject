using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class RegeneratePackageRequestDTO
    {
        [Required(ErrorMessage = "Package ID is required")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [StringLength(1000, ErrorMessage = "Feedback cannot exceed 1000 characters")]
        public string? Feedback { get; set; }

        public List<string> ExcludeActivities { get; set; } = new List<string>();

        [Range(100, 1000000, ErrorMessage = "Budget must be between 100 and 1,000,000")]
        public decimal? NewBudget { get; set; }

        [Range(1, 30, ErrorMessage = "Duration must be between 1 and 30 days")]
        public int? NewDuration { get; set; }
    }

}
