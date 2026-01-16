using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.Country
{
    public class CreateCountryCommand : ICommand<Result<Guid>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
