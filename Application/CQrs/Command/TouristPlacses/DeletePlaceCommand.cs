using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.TouristPlacses
{
    public class DeletePlaceCommand : ICommand<Result<bool>>
    {
        public Guid PlaceId { get; set; }
    }
}
