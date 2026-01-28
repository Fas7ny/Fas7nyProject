namespace Fas7ny.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string BookingType { get; set; } = string.Empty;
        public int BookingItemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public virtual ApplicationUser User { get; set; } = null!;
        public Payment? Payment { get; set; }
        public ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
    }

}
