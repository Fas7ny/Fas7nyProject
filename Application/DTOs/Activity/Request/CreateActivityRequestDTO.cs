namespace Fas7ny.Application.DTOs.Activity.Request
{
    public class CreateActivityRequestDTO
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int CityId { get; set; }
        public string ImageUrl { get; set; }
    }
}
