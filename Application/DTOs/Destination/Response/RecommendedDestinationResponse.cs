namespace Fas7ny.Application.DTOs.Destination.Response
{
    public class RecommendedDestinationResponse
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public double MatchScore { get; set; }
    }
}
