namespace Fas7ny.Application.DTOs.Activity.Response
{
    public class BookActivityResponse
    {
        public int BookingId { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public int PaymentId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
