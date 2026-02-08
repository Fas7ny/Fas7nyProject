namespace Fas7ny.Domain.Entities
{
    public class Payment
    {
        public string Id { get; set; }

        public int BookingId { get; set; }
        public Booking Book { get; set; } = null!;
        public int PaymobOrderId { get; set; }

        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";

        public string PaymentMethod { get; set; } = "Paymob";
        public DateTime PaymentDate { get; set; }

        public string PaymentUrl { get; set; } = null!;
    }

}
