using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class UserBehaviorAnalysisRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [Range(1, 365, ErrorMessage = "Analysis period must be between 1 and 365 days")]
        public int AnalysisPeriodDays { get; set; } = 90;

        public bool IncludePredictions { get; set; } = true;

        public bool IncludeRecommendations { get; set; } = true;

    }
}
