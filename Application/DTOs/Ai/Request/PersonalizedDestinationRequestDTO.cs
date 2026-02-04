namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class PersonalizedDestinationRequestDTO
    {
        public string Query { get; set; } = string.Empty;
        public string? Country { get; set; }
        public string? Language { get; set; }
        public int Limit { get; set; } = 5;
    }
}
