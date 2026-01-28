namespace Fas7ny.Domain.Entities
{
    public class Carts
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


    }
}
