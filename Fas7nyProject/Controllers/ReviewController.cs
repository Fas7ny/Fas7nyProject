using Fas7ny.Application.DTOs.Review.Request;
using Fas7ny.Application.DTOs.Review.Response;
using Fas7ny.Application.ServiceInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
           [FromForm] CreateReviewRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            var Review = new Review
            {
                Comment = dto.Comment,
                Rating = dto.Rating,

            };

            await _unitOfWork.Reviews.AddAsync(Review);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = Review.Id },
                MapToDto(Review)
            );
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid review id");

            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
                return NotFound($"Reviews with id {id} not found");

            return Ok(MapToDto(review));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewRequest dto)
        {
            if (id <= 0)
                return BadRequest("Invalid review id");

            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
                return NotFound($"Reviews with id {id} not found");

            review.Comment = dto.Comment;
            review.Rating = dto.Rating;


            await _unitOfWork.Reviews.UpdateAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return Ok(MapToDto(review));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid review id");

            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
                return NotFound($"Reviews with id {id} not found");

            await _unitOfWork.Reviews.DeleteAsync(review);
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

                var (reviews, totalCount) =
                    await _unitOfWork.Reviews.GetPagedAsync(page.Value, pageSize.Value);

                return Ok(new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                    items = reviews.Select(MapToDto)
                });
            }

            var allReviews = await _unitOfWork.Reviews.GetAllAsync();
            return Ok(allReviews.Select(MapToDto));
        }
        private static ReviewDetailsResponse MapToDto(Review review) => new()
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
        };
    }
}
