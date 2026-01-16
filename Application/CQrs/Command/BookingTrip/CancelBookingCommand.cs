using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.BookingTrip
{
    public class CancelBookingCommand : ICommand<Result<Guid>>
    {
        public Guid BookingId { get; set; }
    }
}
