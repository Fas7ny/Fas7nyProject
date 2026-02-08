using System.ComponentModel.DataAnnotations.Schema;

namespace Fas7ny.Domain.Entities
{
    public class BookingCustomTrip
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string DestinationCity { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NumberOfTravelers { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ================= Relations =================

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public Payment? Payment { get; set; }
    }
}
