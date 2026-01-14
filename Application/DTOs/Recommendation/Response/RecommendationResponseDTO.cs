using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.DTOs.Recommendation.Response
{
    public class RecommendationResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecommendedItemType { get; set; } = string.Empty;
        public int ItemId { get; set; }
        public string? Reason { get; set; }

        public virtual UserResponseDto User { get; set; } = null!;
    }
}
