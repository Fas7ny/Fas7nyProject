using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.Booking.Response;

namespace Fas7ny.Application.CQrs.Query.Booking
{
    public class GetUserBookingsQuery : IQuery<Result<List<BookingResponseDTO>>>
    {
        public Guid UserId { get; set; }
    }
}
