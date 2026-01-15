namespace Fas7ny.Application.DTOs.Transportations.FlightDtos.Response
{
    public class FlightResponseDTO
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public int DepartureCityId { get; set; }
        public string DepartureCityName { get; set; }
        public int ArrivalCityId { get; set; }
        public string ArrivalCityName { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public TimeSpan Duration { get; set; }
    }   
}
