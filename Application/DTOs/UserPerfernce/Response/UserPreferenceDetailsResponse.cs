using Fas7ny.Application.DTOs.Ai.Response;
using Fas7ny.Application.DTOs.Destination.Response;

namespace Fas7ny.Application.DTOs.UserPerfernce.Response
{
    public class UserPreferenceDetailsResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public int StayDuration { get; set; }
        public decimal Budget { get; set; }
        public string CategoryPreference { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<RecommendationResponseDTO> RecommendedPackages { get; set; } = new List<RecommendationResponseDTO>();
        public List<RecommendedDestinationResponse> RecommendedDestinations { get; set; } = new List<RecommendedDestinationResponse>();
    }
}
