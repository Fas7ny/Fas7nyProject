using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.TouristPlacses
{
    public class UpdatePlaceCommand : ICommand<Result<bool>>
    {
        public Guid PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CityId { get; set; }
    }
}
