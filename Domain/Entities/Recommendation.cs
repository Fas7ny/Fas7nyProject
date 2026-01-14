namespace Fas7ny.Domain.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecommendedItemType { get; set; } = string.Empty;
        public int ItemId { get; set; }
        public string? Reason { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
