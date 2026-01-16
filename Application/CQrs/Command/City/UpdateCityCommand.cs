using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.City
{
    public class UpdateCityCommand : ICommand<Result<bool>>
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
