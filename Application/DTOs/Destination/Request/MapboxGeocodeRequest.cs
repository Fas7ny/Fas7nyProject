namespace Fas7ny.Application.DTOs.Destination.Request
{
    public class MapboxGeocodeRequest
    {
        public string Query { get; set; }
        public string? Country { get; set; }
        public string? Language { get; set; }
        public int Limit { get; set; } = 5;
    }

}
