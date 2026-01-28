using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class PersonalizedDestinationRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [Range(1, 20, ErrorMessage = "Number of destinations must be between 1 and 20")]
        public int NumberOfDestinations { get; set; } = 5;

        [Range(100, 1000000, ErrorMessage = "Budget must be between 100 and 1,000,000")]
        public decimal? Budget { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TravelDate { get; set; }

        public List<string> MustHaveFeatures { get; set; } = new List<string>();
    }
}
