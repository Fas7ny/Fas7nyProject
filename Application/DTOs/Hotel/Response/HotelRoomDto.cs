namespace Fas7ny.Application.DTOs.Hotel.Response
{
    public class HotelRoomDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
    }
}
