using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Transportations.FlightDtos.Request
{
   public class CreateFlightRequestDTO
    {
        [Required(ErrorMessage = "Flight number is required")]
        [StringLength(20, ErrorMessage = "Flight number cannot exceed 20 characters")]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "Airline is required")]
        [StringLength(100, ErrorMessage = "Airline cannot exceed 100 characters")]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Departure city ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Departure City ID is required")]
        public int DepartureCityId { get; set; }

        [Required(ErrorMessage = "Arrival city ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Arrival City ID is required")]
        public int ArrivalCityId { get; set; }

        [Required(ErrorMessage = "Departure time is required")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Arrival time is required")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Available seats is required")]
        [Range(0, 500, ErrorMessage = "Available seats must be between 0 and 500")]
        public int AvailableSeats { get; set; }
    }
}
