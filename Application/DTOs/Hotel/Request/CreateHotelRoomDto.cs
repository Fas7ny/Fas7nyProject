using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Hotel.Request
{
    public class CreateHotelRoomDto
    {
        [Required(ErrorMessage = "Hotel ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid hotel ID is required")]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [StringLength(50, ErrorMessage = "Room type cannot exceed 50 characters")]
        public string RoomType { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public bool Available { get; set; } = true;
    }
}
