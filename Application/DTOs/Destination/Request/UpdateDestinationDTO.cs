namespace Fas7ny.Application.DTOs.Destination.Request
{
    public class UpdateDestinationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Rating { get; set; }
    }
}
