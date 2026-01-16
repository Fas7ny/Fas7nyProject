using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.TouristPlacses
{
    public class CreatePlaceCommand : ICommand<Result<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CityId { get; set; }
    }
}
