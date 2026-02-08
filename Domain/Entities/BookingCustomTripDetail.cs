using System.ComponentModel.DataAnnotations.Schema;

namespace Fas7ny.Domain.Entities
{
    public class BookingCustomTripDetail
    {
        public int Id { get; set; }

        public int BookingCustomTripId { get; set; }
        public int CityId { get; set; }
        public int HotelId { get; set; }

        public string PackageName { get; set; } = null!;

        public string DestinationCity { get; set; } = null!;

        public int DurationDays { get; set; }

        public decimal PricePerPerson { get; set; }

        public decimal TotalPrice { get; set; }

        public int AccommodationRating { get; set; }

        public bool IncludeFlights { get; set; }

        public bool IncludeMeals { get; set; }

        public bool IncludeTransportation { get; set; }

        public bool IncludeGuide { get; set; }

        public string? Activities { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ================= Relations =================

        [ForeignKey(nameof(BookingCustomTripId))]
        public BookingCustomTrip BookingCustomTrip { get; set; } = null!;
        public City City { get; set; }
        public Hotel Hotel { get; set; }
    }
}
