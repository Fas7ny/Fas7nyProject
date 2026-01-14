namespace Fas7ny.Application.DTOs.Transportations.FlightDtos.Response
{
    public class FlightResponseDTO
    {
        public string AirLine { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }

        public DateTime ArivaTime { get; set; }
    }
}
