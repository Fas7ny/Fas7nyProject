using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.City
{
    public class DeleteCityCommand : ICommand<Result<bool>>
    {
        public Guid CityId { get; set; }
    }
}
