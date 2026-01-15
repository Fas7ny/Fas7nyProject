using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Hotel.Request
{
    public class CreateHotelRoomDTO
    {
        [Required(ErrorMessage = "Hotel ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Hotel ID is required")]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [StringLength(50, ErrorMessage = "Room type cannot exceed 50 characters")]
        public string RoomType { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Price per night is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Availability status is required")]
        public bool IsAvailable { get; set; }
    }
}
