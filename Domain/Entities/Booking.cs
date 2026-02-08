namespace Fas7ny.Domain.Entities
{
    public class Booking
    {
        public int orderId;

        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string BookingType { get; set; } = null!;

        public int? BookingItemId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;

        public Payment? Payment { get; set; }
        public ICollection<CartItems> CartItems { get; set; }
    }
}
