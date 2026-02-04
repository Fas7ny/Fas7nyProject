using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IUnitOfWork context, ILogger<ProfileController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("user-profile/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            _logger.LogInformation("Fetching profile for user {UserId}", userId);

            var allUserPrefs = await _context.UserPreferences.GetAllAsync();
            var userPref = allUserPrefs.FirstOrDefault(u => u.UserId == userId);

            if (userPref == null)
            {
                return NotFound(new { message = $"User preferences with ID {userId} not found" });
            }

            var allBookings = await _context.Bookings.GetAllAsync();
            var bookingsList = allBookings.Where(b => b.UserId == userId).ToList();

            var allReviews = await _context.Reviews.GetAllAsync();
            var reviewsList = allReviews.Where(r => r.UserId == userId).ToList();

            var profile = new
            {
                userId = userPref.UserId,
                username = userPref.Username,

                memberSince = userPref.CreatedAt,
                statistics = new
                {
                    totalBookings = bookingsList.Count(),
                    totalReviews = reviewsList.Count(),
                    averageRating = reviewsList.Any() ? reviewsList.Average(r => r.Rating) : 0
                },
                recentBookings = bookingsList
                    .OrderByDescending(b => b.CreatedAt)
                    .Take(5)
                    .Select(b => new
                    {
                        bookingId = b.Id,

                        bookingDate = b.CreatedAt,
                        status = b.Status,
                        totalPrice = b.TotalPrice
                    }),
                recentReviews = reviewsList
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(5)
                    .Select(r => new
                    {
                        reviewId = r.Id,
                        rating = r.Rating,
                        comment = r.Comment,
                        createdAt = r.CreatedAt
                    }),
                preferences = new
                {
                    budget = userPref.Budget,
                    stayDuration = userPref.StayDuration,
                    categoryPreference = userPref.CategoryPreference
                }
            };

            return Ok(profile);
        }



        [HttpGet("booking-history/{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingHistory(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching booking history for user {UserId}", userId);

            var allBookings = await _context.Bookings.GetAllAsync();
            var bookingsList = allBookings.Where(b => b.UserId == userId).ToList();

            var totalCount = bookingsList.Count();
            var bookings = bookingsList
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new
                {
                    bookingId = b.Id,
                    bookingDate = b.CreatedAt,
                    status = b.Status,
                    totalPrice = b.TotalPrice,
                    startDate = b.StartDate,
                    endDate = b.EndDate
                });

            return Ok(new
            {
                success = true,
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                bookings
            });
        }



    }
}