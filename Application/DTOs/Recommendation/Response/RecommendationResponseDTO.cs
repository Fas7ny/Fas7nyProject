using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.DTOs.Recommendation.Response
{
    public class RecommendationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string RecommendationType { get; set; }
        public decimal Score { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
