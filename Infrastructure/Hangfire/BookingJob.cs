using Fas7ny.Domain.Enum;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.Extensions.Logging;

namespace Fas7ny.Infrastructure.Hangfire
{
    public class BookingJob : IBookingJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingJob> _logger;

        public BookingJob(
            IUnitOfWork unitOfWork,
            ILogger<BookingJob> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task ConfirmBooking(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);

            if (booking == null)
                return;

            if (booking.Status == BookingStatus.Confirmed.ToString())
                return;

            booking.Status = BookingStatus.Confirmed.ToString();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
