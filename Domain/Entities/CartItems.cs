namespace Fas7ny.Domain.Entities
{
    public class CartItems
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Carts? Cart { get; set; }
        public Guid ProductId { get; set; }
        public Booking? Product { get; set; }
        public int Quantity { get; set; }
        public int BookingId { get; set; }
        public decimal Price { get; set; }
        public Booking Booking { get; set; }

    }
}
