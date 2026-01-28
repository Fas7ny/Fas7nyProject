namespace Fas7ny.Application.DTOs.Ai.Response
{
    public class UserBehaviorAnalysisResponseDTO
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public UserBehaviorProfile Profile { get; set; }
        public List<TravelPattern> TravelPatterns { get; set; } = new List<TravelPattern>();
        public List<Prediction> Predictions { get; set; } = new List<Prediction>();
        public List<RecommendationItem> Recommendations { get; set; } = new List<RecommendationItem>();
        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }

    public class UserBehaviorProfile
    {
        public string TravelPersonality { get; set; }
        public List<string> PreferredDestinations { get; set; } = new List<string>();
        public List<string> PreferredActivities { get; set; } = new List<string>();
        public decimal AverageBudget { get; set; }
        public int AverageTripDuration { get; set; }
        public string PreferredSeason { get; set; }
        public string AccommodationPreference { get; set; }
        public Dictionary<string, int> InteractionPatterns { get; set; } = new Dictionary<string, int>();
    }

    public class TravelPattern
    {
        public string PatternType { get; set; }
        public string Description { get; set; }
        public double Confidence { get; set; }
        public List<string> SupportingEvidence { get; set; } = new List<string>();
    }

    public class Prediction
    {
        public string PredictionType { get; set; }
        public string Description { get; set; }
        public double Probability { get; set; }
        public DateTime? PredictedDate { get; set; }
        public List<string> Factors { get; set; } = new List<string>();
    }
}
