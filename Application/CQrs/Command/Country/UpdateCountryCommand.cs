using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.Country
{
    public class UpdateCountryCommand : ICommand<Result<bool>>
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
