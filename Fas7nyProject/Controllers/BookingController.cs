using Fas7ny.Application.DTOs.Booking.Request;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fas7nyProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BookingController> _logger;

        public BookingController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ILogger<BookingController> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(
            [FromBody] CreateBookingRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                User.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);

            if (dto.EndDate <= dto.StartDate)
                return BadRequest(new { message = "End date must be after start date" });

            var booking = new Booking
            {
                UserId = userId!,
                BookingType = dto.BookingType,
                BookingItemId = dto.BookingItemId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "Pending",
                TotalPrice = CalculatePrice(dto),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                booking.Id,
                booking.TotalPrice,
                booking.Status
            });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && booking.UserId != userId)
                return Forbid();

            return Ok(booking);
        }

        [Authorize]
        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings(
            [FromQuery] string? status = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = _unitOfWork.Bookings
                .Query()
                .Where(b => b.UserId == userId);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(b => b.Status == status);

            var result = await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (booking.UserId != userId)
                return Forbid();

            if (booking.Status is "Cancelled" or "Completed")
                return BadRequest(new { message = "Cannot cancel this booking" });

            var deadline = booking.StartDate.AddHours(-24);
            if (DateTime.UtcNow >= deadline)
                return BadRequest(new
                {
                    message = "Cannot cancel within 24 hours of start date"
                });

            booking.Status = "Cancelled";
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                booking.Status
            });
        }

        // =========================
        // ADMIN ENDPOINTS
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings(
            [FromQuery] string? bookingType,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _unitOfWork.Bookings.Query();

            if (!string.IsNullOrWhiteSpace(bookingType))
                query = query.Where(b => b.BookingType == bookingType);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                data
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateBookingStatus(
            int id,
            [FromBody] UpdateBookingStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            booking.Status = dto.Status;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                booking.Id,
                booking.Status
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();

            var result = new
            {
                TotalBookings = bookings.Count(),
                Pending = bookings.Count(b => b.Status == "Pending"),
                Confirmed = bookings.Count(b => b.Status == "Confirmed"),
                Cancelled = bookings.Count(b => b.Status == "Cancelled"),
                Completed = bookings.Count(b => b.Status == "Completed"),
                TotalRevenue = bookings
                    .Where(b => b.Status == "Completed")
                    .Sum(b => b.TotalPrice)
            };

            return Ok(result);
        }

        // =========================
        // HELPERS
        // =========================

        private static decimal CalculatePrice(CreateBookingRequestDTO dto)
        {
            var days = (dto.EndDate - dto.StartDate).Days;
            if (days <= 0) days = 1;

            return days * 1000;
        }
    }
}
