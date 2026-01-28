namespace Fas7ny.Application.DTOs.UserInteraction.Response
{
    public class UserInteractionDetailsResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string ItemType { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImageUrl { get; set; }
        public string InteractionType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
