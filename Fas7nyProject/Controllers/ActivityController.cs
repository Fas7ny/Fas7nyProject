using Fas7ny.Application.DTOs.Activity.Request;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServiceInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ActivityController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
             IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        #region touristPlaceId
        //[HttpGet("tourist-places/{touristPlaceId:int}/activities")]
        //public async Task<IActionResult> GetActivitiesByTouristPlaceId(int touristPlaceId)
        //{
        //    if (touristPlaceId <= 0)
        //        return BadRequest("Invalid touristPlaceId");

        //    var activities = await _unitOfWork.Activities.GetByTouristPlaceIdAsync(touristPlaceId);

        //    if (!activities.Any())
        //        return NotFound($"No activities found for TouristPlaceId {touristPlaceId}");

        //    var response = activities.Select(a => new ActivityResponseDTO
        //    {
        //        Id = a.Id,
        //        Name = a.Name,
        //        PictureUrl = a.ImageUrl
        //    });

        //    return Ok(response);
        //}

        #endregion

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var activity = new Activity(
                dto.Name,
                dto.Price,
                dto.CityId

                );

            await _unitOfWork.Activities.AddAsync(activity);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = activity.Id },
                new
                {
                    activity.Id,
                    activity.Name,
                    Price = activity.Cost,
                    activity.CityId
                });
        }


        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("id not valid");
            var activity = await _unitOfWork.Activities.GetByIdAsync(id);
            if (activity == null)
                return NotFound(new { message = "Activity not found" });
            return Ok(new
            {
                activity.Id,
                activity.Name,
                Price = activity.Cost,
                activity.CityId,
                PictureUrl = ImageUrlHelper.BuildImageUrl(
        "http://Fas7ny.runasp.net",
        "Activity",
        activity.PictureUrl
              )
            });
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _unitOfWork.Activities.GetAllAsync();

            var response = activities.Select(a => new
            {
                a.Id,
                a.Name,
                Price = a.Cost,
                a.CityId,
                a.ImageUrl
            });

            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateActivityRequestDTO dto, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var activity = await _unitOfWork.Activities.GetByIdAsync(id);

            if (image != null)
            {
                if (!string.IsNullOrEmpty(activity.ImageUrl))
                {
                    await _fileService.DeleteFileAsync(activity.ImageUrl);
                }
                activity.ImageUrl = await _fileService.SaveFileAsync(image, "Activity");

            }

            if (activity == null)
                return NotFound(new { message = "Activity not found" });

            activity.Update(dto.Name, dto.Price, dto.CityId);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                activity.Id,
                activity.Name,
                Price = activity.Cost,
                activity.CityId,
                Message = "Activity updated successfully",
                activity.ImageUrl
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _unitOfWork.Activities.GetByIdAsync(id);
            if (activity == null)
                return NotFound(new { message = "Activity not found" });

            await _unitOfWork.Activities.DeleteAsync(activity);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { message = "Activity deleted successfully" });
        }

        [HttpGet("cities/{cityId:int}/activities")]
        public async Task<IActionResult> GetActivitiesByCityId(int cityId)
        {
            if (cityId <= 0)
                return BadRequest("Invalid city ID");

            var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
            if (city == null)
                return NotFound("City not found");

            // Get all activities and filter by cityId
            var allActivities = await _unitOfWork.Activities.GetAllAsync();
            var cityActivities = allActivities.Where(a => a.CityId == cityId).ToList();

            var response = cityActivities.Select(r => new
            {
                r.Id,
                r.Name,
                r.CityId,
                PictureUrl = ImageUrlHelper.BuildImageUrl(
                       "http://Fas7ny.runasp.net",
                        "Activity",
                        r.PictureUrl
                               )
            }).ToList();

            return Ok(response);



        }

    }
}