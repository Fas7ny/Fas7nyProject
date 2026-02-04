using Fas7ny.Application.DTOs.Booking.Request;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("Create-Booking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation("Starting booking creation for user {UserId}", dto.UserId);

                var user = await _userManager.FindByIdAsync(dto.UserId);
                if (user == null)
                {
                    _logger.LogWarning("User {UserId} not found", dto.UserId);
                    return NotFound(new { message = "User not found" });
                }

                if (dto.EndDate <= dto.StartDate)
                {
                    return BadRequest(new { message = "End date must be after start date" });
                }

                var minStartDate = DateTime.Now.AddMinutes(1);
                if (dto.StartDate < minStartDate)
                {
                    return BadRequest(new
                    {
                        message = "Start date must be in the future",
                        currentTime = DateTime.Now,
                        yourStartDate = dto.StartDate
                    });
                }

                _logger.LogInformation("Validation passed, creating booking entity");

                var booking = new Booking
                {
                    UserId = dto.UserId,
                    BookingType = dto.BookingType ?? "General",
                    BookingItemId = dto.BookingItemId.ToString(),
                    StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Unspecified),
                    EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Unspecified),
                    TotalPrice = dto.TotalAmount,
                    Status = "0", // Pending
                    CreatedAt = DateTime.Now
                };

                _logger.LogInformation("Adding booking to repository");
                await _unitOfWork.Bookings.AddAsync(booking);

                _logger.LogInformation("Saving changes to database");
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Booking {BookingId} created successfully for user {UserId}",
                    booking.Id, dto.UserId);

                return CreatedAtAction(
                    nameof(GetBookingById),
                    new { id = booking.Id },
                    new
                    {
                        booking.Id,
                        booking.UserId,
                        booking.BookingType,
                        booking.BookingItemId,
                        booking.StartDate,
                        booking.EndDate,
                        booking.TotalPrice,
                        booking.Status,
                        Message = "Booking created successfully"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking. Exception: {Message}, StackTrace: {StackTrace}",
                    ex.Message, ex.StackTrace);

                return StatusCode(500, new
                {
                    message = "An error occurred while creating the booking",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
                if (booking == null)
                    return NotFound(new { message = "Booking not found" });

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin");


                if (!isAdmin && booking.UserId != userId)
                    return Forbid();

                return Ok(new
                {
                    booking.Id,
                    booking.UserId,
                    booking.BookingType,
                    booking.BookingItemId,
                    booking.StartDate,
                    booking.EndDate,
                    booking.TotalPrice,
                    booking.Status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving booking {BookingId}", id);
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the booking",
                    error = ex.Message
                });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings(
            [FromQuery] string? status = null,
            [FromQuery] string? bookingType = null,
            [FromQuery] int? page = 1,
            [FromQuery] int? pageSize = 10)
        {
            try
            {
                var allBookings = await _unitOfWork.Bookings.GetAllAsync();


                if (!string.IsNullOrEmpty(status))
                    allBookings = allBookings.Where(b => b.Status == status);

                if (!string.IsNullOrEmpty(bookingType))
                    allBookings = allBookings.Where(b => b.BookingType == bookingType);


                var totalCount = allBookings.Count();
                var bookings = allBookings
                    .OrderByDescending(b => b.StartDate)
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .Select(b => new
                    {
                        b.Id,
                        b.UserId,
                        b.BookingType,
                        b.BookingItemId,
                        b.StartDate,
                        b.EndDate,
                        b.TotalPrice,
                        b.Status
                    })
                    .ToList();

                return Ok(new
                {
                    TotalCount = totalCount,
                    Page = page.Value,
                    PageSize = pageSize.Value,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                    Bookings = bookings
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all bookings");
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving bookings",
                    error = ex.Message
                });
            }
        }


        [Authorize]
        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings([FromQuery] string? status = null)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var allBookings = await _unitOfWork.Bookings.GetAllAsync();

                var userBookings = allBookings.Where(b => b.UserId == userId);


                if (!string.IsNullOrEmpty(status))
                    userBookings = userBookings.Where(b => b.Status == status);

                var response = userBookings
                    .OrderByDescending(b => b.StartDate)
                    .Select(b => new
                    {
                        BookingId = b.Id,
                        b.BookingType,
                        b.BookingItemId,
                        b.StartDate,
                        b.EndDate,
                        b.TotalPrice,
                        b.Status
                    })
                    .ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user bookings");
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving your bookings",
                    error = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateBookingStatus(
            int id,
            [FromBody] UpdateBookingStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
                if (booking == null)
                    return NotFound(new { message = "Booking not found" });

                var validStatuses = new[] { "Pending", "Confirmed", "Cancelled", "Completed" };
                if (!validStatuses.Contains(dto.Status))
                    return BadRequest(new { message = "Invalid status. Valid statuses: Pending, Confirmed, Cancelled, Completed" });

                booking.Status = dto.Status;
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Booking {BookingId} status updated to {Status}",
                    id, dto.Status);

                return Ok(new
                {
                    booking.Id,
                    booking.Status,
                    Message = $"Booking status updated to {dto.Status}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking status");
                return StatusCode(500, new
                {
                    message = "An error occurred while updating booking status",
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
                if (booking == null)
                    return NotFound(new { message = "Booking not found" });

                var isAdmin = User.IsInRole("Admin");
                if (!isAdmin && booking.UserId != userId)
                    return Forbid();

                if (booking.Status == "Cancelled")
                    return BadRequest(new { message = "Booking is already cancelled" });

                if (booking.Status == "Completed")
                    return BadRequest(new { message = "Cannot cancel a completed booking" });

                // ⏱️ UTC-safe cancellation check
                var cancellationDeadline = booking.StartDate.AddHours(-24);

                if (DateTime.UtcNow >= cancellationDeadline && !isAdmin)
                {
                    return BadRequest(new
                    {
                        message = "Cannot cancel within 24 hours of booking date. Please contact support.",
                        cancellationDeadlineUtc = cancellationDeadline
                    });
                }

                booking.Status = "Cancelled";

                // Explicit save
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Booking {BookingId} cancelled by user {UserId}",
                    id, userId);

                return Ok(new
                {
                    message = "Booking cancelled successfully",
                    bookingId = booking.Id,
                    status = booking.Status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling booking {BookingId}", id);
                return StatusCode(500, new
                {
                    message = "An error occurred while cancelling the booking"
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
                if (booking == null)
                    return NotFound(new { message = "Booking not found" });

                await _unitOfWork.Bookings.DeleteAsync(booking);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Booking {BookingId} deleted", id);

                return Ok(new { message = "Booking deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking {BookingId}", id);
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the booking",
                    error = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics")]
        public async Task<IActionResult> GetBookingStatistics()
        {
            try
            {
                var allBookings = await _unitOfWork.Bookings.GetAllAsync();

                var statistics = new
                {
                    TotalBookings = allBookings.Count(),
                    PendingBookings = allBookings.Count(b => b.Status == "Pending"),
                    ConfirmedBookings = allBookings.Count(b => b.Status == "Confirmed"),
                    CancelledBookings = allBookings.Count(b => b.Status == "Cancelled"),
                    CompletedBookings = allBookings.Count(b => b.Status == "Completed"),
                    TotalRevenue = allBookings
                        .Where(b => b.Status == "Completed")
                        .Sum(b => b.TotalPrice),
                    BookingsByType = allBookings
                        .GroupBy(b => b.BookingType)
                        .Select(g => new { Type = g.Key, Count = g.Count() })
                        .ToList()
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving booking statistics");
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving statistics",
                    error = ex.Message
                });
            }
        }

        [HttpPut("update-booking/{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookingStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);

            if (booking == null)
                return NotFound($"Booking with id {id} not found");

            booking.Status = dto.Status;

            await _unitOfWork.Bookings.UpdateAsync(booking);
            _unitOfWork.SaveChanges();

            return Ok(booking);
        }

    }
}