namespace Fas7ny.Application.DTOs.Ai.Response
{
    public class AiChatResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public List<QuickAction> SuggestedActions { get; set; } = new List<QuickAction>();
        public List<RecommendationItem> RelatedRecommendations { get; set; } = new List<RecommendationItem>();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string ConversationId { get; set; }
    }
    public class QuickAction
    {
        public string ActionType { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
