using Fas7ny.Application.DTOs.Hotel.Request;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServiceInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        public HotelController(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("hotel")]
        public async Task<IActionResult> CreateHotel(
               [FromForm] CreateHotelDto dto,
               IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? imagePath = null;

            if (image != null)
                imagePath = await _fileService.SaveFileAsync(image, "Hotel");

            var hotel = new Hotel
            {
                Name = dto.Name,
                CityId = dto.CityId,
                Description = dto.Description,
                ImageUrl = imagePath,
                Address = dto.Address,
                CategoryId = dto.CategoryId
            };

            await _unitOfWork.Hotels.AddAsync(hotel);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetHotelById),
                new { id = hotel.Id },
                hotel
            );
        }




        [Authorize(Roles = "Admin")]
        [HttpPost("hotels-room")]
        public async Task<IActionResult> CreateHotelRoom(
              [FromForm] CreateHotelRoomDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var hotelRoom = new HotelRoom
            {
                HotelId = dto.HotelId,
                RoomType = dto.RoomType,
                Capacity = dto.Capacity,
                Price = dto.PricePerNight,
                Available = dto.IsAvailable,

            };

            await _unitOfWork.HotelRooms.AddAsync(hotelRoom);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                            nameof(GetHotelRoomById),
                            new { id = hotelRoom.Id },
                            hotelRoom
                        );
        }



        [HttpGet("hotels/{id:int}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid hotel id");

            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });

            return Ok(new
            {
                hotel.Id,
                hotel.Name,
                hotel.Description,
                hotel.Address,
                hotel.CityId,

                PictureUrl = ImageUrlHelper.BuildImageUrl(
        "http://Fas7ny.runasp.net",
        "hotel",
        hotel.ImageUrl
    )
            });
        }




        [HttpGet("hotel-rooms/{id:int}")]
        public async Task<IActionResult> GetHotelRoomById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid hotel room id");

            var room = await _unitOfWork.HotelRooms.GetByIdAsync(id);
            if (room == null)
                return NotFound(new { message = "Hotel room not found" });

            return Ok(new
            {
                room.Id,
                room.HotelId,
                room.RoomType,
                room.Capacity,
                room.Price,
                room.Available,


            });
        }



        [HttpGet("hotels/{hotelId:int}/rooms")]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
        {
            if (hotelId <= 0)
                return BadRequest("Invalid hotel id");

            var hotel = await _unitOfWork.Hotels.GetByIdAsync(hotelId);
            if (hotel == null)
                return NotFound("Hotel not found");

            var rooms = await _unitOfWork.HotelRooms.GetAllAsync();
            var hotelRooms = rooms.Where(r => r.HotelId == hotelId).ToList();

            if (!hotelRooms.Any())
                return Ok(new { message = "No rooms found for this hotel", rooms = new List<object>() });

            var roomDtos = hotelRooms.Select(room => new
            {
                room.Id,
                room.HotelId,
                room.RoomType,
                room.Capacity,
                room.Price,
                room.Available
            });

            return Ok(roomDtos);
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("hotel/{id:int}")]
        public async Task<IActionResult> UpdateHotel(
        int id,
        [FromForm] UpdateHotelDto dto,
         IFormFile? image)
        {
            if (id <= 0)
                return BadRequest("Invalid hotel id");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });


            hotel.Name = dto.Name;
            hotel.Description = dto.Description;
            hotel.Address = dto.Address;
            hotel.PricePerNight = dto.price;


            if (image != null)
            {
                if (!string.IsNullOrEmpty(hotel.ImageUrl))
                    await _fileService.DeleteFileAsync(hotel.ImageUrl);

                hotel.ImageUrl = await _fileService.SaveFileAsync(image, "Hotel");
            }

            await _unitOfWork.Hotels.UpdateAsync(hotel);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                hotel.Id,
                hotel.Name,
                hotel.Description,
                hotel.Address,
                hotel.PricePerNight,
                hotel.ImageUrl
            });
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("hotelroom/{id:int}")]
        public async Task<IActionResult> UpdateHotelRoom(int id,
    [FromForm] UpdateHotelRoomDTO dto
     )
        {
            if (id <= 0)
                return BadRequest("Invalid hotel id");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = await _unitOfWork.HotelRooms.GetByIdAsync(id);
            if (room == null)
                return NotFound(new { message = "Hotel not found" });

            room.Capacity = dto.Capacity;
            room.Available = dto.IsAvailable;
            room.Price = dto.PricePerNight;
            room.RoomType = dto.RoomType;





            await _unitOfWork.HotelRooms.UpdateAsync(room);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                room.Id,
                room.Price,
                room.Capacity,
                room.Available,

            });
        }


    }
}
