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

        #region CRUD

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromForm] CreateRestaurantRequestDTO dto,
            IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            string? imagePath = null;
            if (image != null)
                imagePath = await _fileService.SaveFileAsync(image, "restaurant");

            var restaurant = new Restaurant
            {
                Name = dto.Name,
                address = dto.Address,
                CityId = dto.CityId,
                Cuisine = dto.CuisineType,
                Description = dto.description,
                CategoryId = dto.CategoryId,
                imageUrl = imagePath
            };

            await _unitOfWork.Restaurants.AddAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            // ✅ Load City before mapping
            var createdRestaurant =
                await _unitOfWork.Restaurants.GetByIdWithIncludesAsync(
                    restaurant.Id,
                    r => r.City
                );

            return CreatedAtAction(
                nameof(GetById),
                new { id = restaurant.Id },
                MapToDto(createdRestaurant!)
            );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid restaurant id");

            var restaurant =
                await _unitOfWork.Restaurants.GetByIdWithIncludesAsync(
                    id,
                    r => r.City
                );

            if (restaurant == null)
                return NotFound($"Restaurant with id {id} not found");

            return Ok(MapToDto(restaurant));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateRestaurantRequestDTO dto)
        {
            if (id <= 0)
                return BadRequest("Invalid restaurant id");

            var restaurant =
                await _unitOfWork.Restaurants.GetByIdWithIncludesAsync(
                    id,
                    r => r.City
                );

            if (restaurant == null)
                return NotFound($"Restaurant with id {id} not found");

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.PriceRange = dto.PriceRange;
            restaurant.address = dto.Address;

            await _unitOfWork.Restaurants.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            return Ok(MapToDto(restaurant));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid restaurant id");

            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound($"Restaurant with id {id} not found");

            await _unitOfWork.Restaurants.DeleteAsync(restaurant);
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

                var (restaurants, totalCount) =
                    await _unitOfWork.Restaurants
                        .GetPagedWithIncludesAsync(
                            page.Value,
                            pageSize.Value,
                            r => r.City
                        );

                return Ok(new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                    items = restaurants.Select(MapToDto)
                });
            }

            var allRestaurants =
                await _unitOfWork.Restaurants.GetAllWithIncludesAsync(r => r.City);

            return Ok(allRestaurants.Select(MapToDto));
        }

        [HttpGet("cities/{cityId:int}/restaurants")]
        public async Task<IActionResult> GetRestaurantsByCityId(int cityId)
        {
            if (cityId <= 0)
                return BadRequest("Invalid city ID");

            var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
            if (city == null)
                return NotFound("City not found");

            var restaurants =
            await _unitOfWork.Restaurants.FindWithIncludesAsync(
                r => r.CityId == cityId,
                r => r.City
            );

            return Ok(restaurants.Select(MapToDto));
        }

        #endregion

        #region Mapper

        private static RestaurantResponseDTO MapToDto(Restaurant restaurant) => new()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.address,
            CityId = restaurant.CityId,
            CityName = restaurant.City?.Name,
            ImageUrl = ImageUrlHelper.BuildImageUrl(
                "http://Fas7ny.runasp.net",
                "restaurant",
                restaurant.imageUrl)
        };

        #endregion
    }
}
