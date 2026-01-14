namespace Fas7ny.Domain.Entities
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; } = true;

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
