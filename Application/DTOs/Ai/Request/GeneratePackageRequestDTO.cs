using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class GeneratePackageRequestDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string DestinationCity { get; set; } = null!;

        [Range(1, 30)]
        public int DurationDays { get; set; }

        [Range(50, 1_000_000)]
        public decimal Budget { get; set; }

        [Range(1, 20)]
        public int NumberOfTravelers { get; set; }

        [Required]
        public string TravelStyle { get; set; } = null!;

        public DateTime? PreferredStartDate { get; set; }

        public List<string> PreferredActivities { get; set; } = new();

        [Range(1, 5)]
        public int? AccommodationRating { get; set; }

        public bool IncludeFlights { get; set; }

        public bool IncludeMeals { get; set; }

        public bool IncludeTransportation { get; set; }

        public bool IncludeGuide { get; set; }
    }
}
