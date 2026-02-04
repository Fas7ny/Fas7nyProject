namespace Fas7ny.Domain.Entities
{
    public class Payment
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public int BookingId { get; set; }
        public Booking? Book { get; set; }
        public string Status { get; set; }
        public int CustomTripBookingId { get; set; }
    }
}
