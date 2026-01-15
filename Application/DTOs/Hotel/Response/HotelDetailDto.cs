namespace Fas7ny.Application.DTOs.Hotel.Response
{
    public class HotelDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public List<HotelRoomDto> Rooms { get; set; }
    }
}
