namespace Fas7ny.Application.DTOs.CartItem.Response
{
    public class CartItemDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid BookingId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        // Booking Information
        public string BookingType { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImageUrl { get; set; }
    }

}
