namespace Fas7ny.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string BookingType { get; set; } = string.Empty;
        public string BookingItemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public virtual ApplicationUser User { get; set; } = null!;
        public Payment? Payment { get; set; }
        public ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
        public DateTime CreatedAt { get; set; }

        // Remove the empty constructor or make it parameterless
        public Booking()
        {
        }
    }

}
