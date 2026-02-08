using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        private string? GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var userPref = await _context.UserPreferences
                .FindAsync(u => u.UserId == userId);

            if (userPref == null)
                return NotFound();

            var bookings = await _context.Bookings.FindAllAsync(b => b.UserId == userId);
            var reviews = await _context.Reviews.FindAllAsync(r => r.UserId == userId);

            return Ok(new
            {
                userId = userPref.UserId,
                username = userPref.Username,
                memberSince = userPref.CreatedAt,
                statistics = new
                {
                    totalBookings = bookings.Count(),
                    totalReviews = reviews.Count(),
                    averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0
                }
            });
        }


        [Authorize]
        [HttpGet("me/bookings")]
        public async Task<IActionResult> GetMyBookingHistory(
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 10)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var query = _context.Bookings.Query()
                .Where(b => b.UserId == userId);

            var totalCount = await query.CountAsync();

            var bookings = await query
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new
                {
                    b.Id,
                    b.Status,
                    b.TotalPrice,
                    b.StartDate,
                    b.EndDate
                })
                .ToListAsync();

            return Ok(new
            {
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                bookings
            });
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserProfileByAdmin(string userId)
        {
            var userPref = await _context.UserPreferences
                .FindAsync(u => u.UserId == userId);

            if (userPref == null)
                return NotFound();

            var bookings = await _context.Bookings.FindAllAsync(b => b.UserId == userId);
            var reviews = await _context.Reviews.FindAllAsync(r => r.UserId == userId);

            return Ok(new
            {
                userId = userPref.UserId,
                username = userPref.Username,
                bookingsCount = bookings.Count(),
                reviewsCount = reviews.Count()
            });
        }


    }
}