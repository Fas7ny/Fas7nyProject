namespace Fas7ny.Application.DTOs.Ai.Response
{
    public class RecommendationResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<RecommendationItem> Recommendations { get; set; } = new List<RecommendationItem>();
        public RecommendationMetadata Metadata { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
    public class RecommendationItem
    {
        public string ItemType { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double MatchScore { get; set; }
        public double Rating { get; set; }
        public decimal? Price { get; set; }
        public string Location { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string ReasonForRecommendation { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }

    public class RecommendationMetadata
    {
        public string AlgorithmUsed { get; set; }
        public string BasedOn { get; set; }
        public int TotalItemsAnalyzed { get; set; }
        public double AverageMatchScore { get; set; }
        public List<string> ConsideredFactors { get; set; } = new List<string>();
    }

    public class DestinationRecommendationResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DestinationRecommendation> Destinations { get; set; } = new List<DestinationRecommendation>();
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }

    public class DestinationRecommendation
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double MatchScore { get; set; }
        public List<string> Highlights { get; set; } = new List<string>();
        public List<string> RecommendedFor { get; set; } = new List<string>();
        public decimal AverageBudgetPerDay { get; set; }
        public string BestTimeToVisit { get; set; }
        public string Climate { get; set; }
        public int NumberOfAttractions { get; set; }
        public string WhyRecommended { get; set; }
    }

    public class PackageRecommendationResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<PackageRecommendation> Packages { get; set; } = new List<PackageRecommendation>();
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }

    public class PackageRecommendation
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; }
        public double MatchScore { get; set; }
        public double Rating { get; set; }
        public int NumberOfReviews { get; set; }
        public List<string> Inclusions { get; set; } = new List<string>();
        public string HotelName { get; set; }
        public int HotelRating { get; set; }
        public string WhyRecommended { get; set; }
    }

}
