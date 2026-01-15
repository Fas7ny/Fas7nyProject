namespace Fas7ny.Application.DTOs.Activity.Response
{
    public class ActivityResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
