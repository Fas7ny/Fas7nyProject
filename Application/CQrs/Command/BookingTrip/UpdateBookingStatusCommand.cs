using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Domain.Enum;

namespace Fas7ny.Application.CQrs.Command.BookingTrip
{
    public class UpdateBookingStatusCommand : ICommand<Result<bool>>
    {
        public Guid BookingId { get; set; }
        public BookingStatus Status { get; set; }
    }
}
