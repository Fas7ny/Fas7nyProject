using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.Hotel.Request
{
    public class UpdateHotelRoomDto
    {
        [StringLength(50, ErrorMessage = "Room type cannot exceed 50 characters")]
        public string RoomType { get; set; }

        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20")]
        public int? Capacity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

        public bool? Available { get; set; }
    }

}
