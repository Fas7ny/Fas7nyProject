using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Domain.Enum;

namespace Fas7ny.Application.CQrs.Command.TouristPlacses
{
    public class ChangePlaceStatusCommand : ICommand<Result<bool>>
    {
        public Guid PlaceId { get; set; }
        public PlaceStatus Status { get; set; }
    }
}
