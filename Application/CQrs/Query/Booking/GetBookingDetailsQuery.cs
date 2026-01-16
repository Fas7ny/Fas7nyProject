using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.Booking.Response;

namespace Fas7ny.Application.CQrs.Query.Booking
{
    public class GetBookingDetailsQuery : IQuery<Result<BookingResponseDTO>>
    {
        public Guid BookingId { get; set; }
    }

}
