namespace Fas7ny.Application.DTOs.Cart.Response
{
    public class CartItemDetailsResponse
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int BookingId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ItemTotal { get; set; }

        public string? BookingType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
