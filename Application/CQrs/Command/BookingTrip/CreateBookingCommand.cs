using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.BookingTrip
{
    public class CreateBookingCommand : ICommand<Result<Guid>>
    {
        public Guid UserId { get; set; }
        public Guid TouristPlaceId { get; set; }
        public DateTime VisitDate { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
