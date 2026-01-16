using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.City
{
    public class CreateCityCommand : ICommand<Result<Guid>>
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
