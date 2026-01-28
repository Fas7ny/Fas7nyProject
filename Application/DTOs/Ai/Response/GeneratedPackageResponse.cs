namespace Fas7ny.Application.DTOs.Ai.Response
{
    public class GeneratedPackageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GeneratedPackageData Data { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string GenerationId { get; set; } = Guid.NewGuid().ToString();
        public List<string> Warnings { get; set; } = new List<string>();
    }
    public class GeneratedPackageData
    {
        public string PackageName { get; set; }
        public string Description { get; set; }
        public int DurationDays { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PricePerPerson { get; set; }

        // Accommodation
        public HotelRecommendation RecommendedHotel { get; set; }

        // Itinerary
        public List<DayItinerary> DailyItinerary { get; set; } = new List<DayItinerary>();

        // Activities
        public List<ActivityRecommendation> RecommendedActivities { get; set; } = new List<ActivityRecommendation>();

        // Restaurants
        public List<RestaurantRecommendation> RecommendedRestaurants { get; set; } = new List<RestaurantRecommendation>();

        // Cost Breakdown


        // Travel Tips
        public List<string> TravelTips { get; set; } = new List<string>();

        // Best Time to Visit
        public string BestTimeToVisit { get; set; }

        // Weather Info
        public class HotelRecommendation
        {
            public int? HotelId { get; set; }
            public string HotelName { get; set; }
            public string Address { get; set; }
            public int Rating { get; set; }
            public decimal PricePerNight { get; set; }
            public decimal TotalHotelCost { get; set; }
            public List<string> Amenities { get; set; } = new List<string>();
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public double DistanceFromCenter { get; set; }
            public string RoomType { get; set; }
        }

        public class DayItinerary
        {
            public int DayNumber { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public List<ItineraryActivity> Activities { get; set; } = new List<ItineraryActivity>();
            public string MorningActivity { get; set; }
            public string AfternoonActivity { get; set; }
            public string EveningActivity { get; set; }
            public List<string> MealSuggestions { get; set; } = new List<string>();
        }

        public class ItineraryActivity
        {
            public int? TouristPlaceId { get; set; }
            public string ActivityName { get; set; }
            public string Description { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public TimeSpan Duration { get; set; }
            public decimal EstimatedCost { get; set; }
            public string Location { get; set; }
            public string ActivityType { get; set; }
        }

        public class ActivityRecommendation
        {
            public int? TouristPlaceId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal EntryFee { get; set; }
            public string OpeningHours { get; set; }
            public int RecommendedDuration { get; set; }
            public string Category { get; set; }
            public double Rating { get; set; }
            public string ImageUrl { get; set; }
            public string WhyRecommended { get; set; }
        }

        public class RestaurantRecommendation
        {
            public int? RestaurantId { get; set; }
            public string Name { get; set; }
            public string Cuisine { get; set; }
            public string PriceRange { get; set; }
            public double Rating { get; set; }
            public string Description { get; set; }
            public decimal AverageCostPerPerson { get; set; }
            public string WhyRecommended { get; set; }
        }

        public class CostBreakdown
        {
            public decimal AccommodationCost { get; set; }
            public decimal ActivitiesCost { get; set; }
            public decimal MealsCost { get; set; }
            public decimal TransportationCost { get; set; }
            public decimal FlightsCost { get; set; }
            public decimal GuideCost { get; set; }
            public decimal MiscellaneousCost { get; set; }
            public decimal TotalCost { get; set; }
            public decimal CostPerPerson { get; set; }
            public decimal SuggestedBudgetBuffer { get; set; }
        }

        public class WeatherInfo
        {
            public string Season { get; set; }
            public string AverageTemperature { get; set; }
            public string Conditions { get; set; }
            public List<string> WhatToPack { get; set; } = new List<string>();
        }

    }
}
