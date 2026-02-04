using Fas7ny.Application.DTOs.City.Request;
using Fas7ny.Application.DTOs.Resturant.Request;
using Fas7ny.Application.DTOs.Resturant.Response;
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
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public RestaurantController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromForm] CreateCityRequestDTO dto,
             IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? imagePath = null;

            if (image != null)
                imagePath = await _fileService.SaveFileAsync(image, "City");

            var resturant = new Restaurant
            {
                Name = dto.Name,
                Description = dto.Description,
                address = dto.address,
                CityId = dto.CityId,
                Cuisine = dto.Cuisine,
                PriceRange = dto.priceRanage


            };

            await _unitOfWork.Restaurants.AddAsync(resturant);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = resturant.Id },
                MapToDto(resturant)
            );
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid resturant id");

            var resturant = await _unitOfWork.Restaurants.GetByIdAsync(id);
            if (resturant == null)
                return NotFound($"resturant with id {id} not found");

            return Ok(MapToDto(resturant));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRestaurantRequestDTO dto)
        {
            if (id <= 0)
                return BadRequest("Invalid resturant id");

            var resturant = await _unitOfWork.Restaurants.GetByIdAsync(id);
            if (resturant == null)
                return NotFound($"resturant with id {id} not found");

            resturant.Name = dto.Name;
            resturant.Description = dto.Description;
            resturant.PriceRange = dto.PriceRange;
            resturant.address = dto.Address;


            await _unitOfWork.Restaurants.UpdateAsync(resturant);
            await _unitOfWork.SaveChangesAsync();

            return Ok(MapToDto(resturant));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid resturant id");

            var resturant = await _unitOfWork.Restaurants.GetByIdAsync(id);
            if (resturant == null)
                return NotFound($"resturant with id {id} not found");

            await _unitOfWork.Restaurants.DeleteAsync(resturant);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            if (page.HasValue && pageSize.HasValue)
            {
                if (page <= 0 || pageSize <= 0)
                    return BadRequest("Invalid pagination values");

                var (resturants, totalCount) =
                    await _unitOfWork.Restaurants.GetPagedAsync(page.Value, pageSize.Value);

                return Ok(new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                    items = resturants.Select(MapToDto)
                });
            }

            var allResturants = await _unitOfWork.Restaurants.GetAllAsync();
            return Ok(allResturants.Select(MapToDto));
        }

        [HttpGet("cities/{cityId:int}/resturants")]
        public async Task<IActionResult> GetActivitiesByCityId(int cityId)
        {
            if (cityId <= 0)
                return BadRequest("Invalid city ID");

            var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
            if (city == null)
                return NotFound("City not found");

            // Get all resturats and filter by cityId
            var allResturants = await _unitOfWork.Restaurants.GetAllAsync();
            var cityResturants = allResturants.Where(a => a.CityId == cityId).ToList();

            return Ok(cityResturants);
        }

        // MAPPER
        private static RestaurantResponseDTO MapToDto(Restaurant restaurant) => new()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.address,
            CityId = restaurant.CityId,
            CityName = restaurant.City.Name,
            ImageUrl = ImageUrlHelper.BuildImageUrl(
              "http://Fas7ny.runasp.net",
             "resturant",
                  restaurant.imageUrl)


        };
    }
}
