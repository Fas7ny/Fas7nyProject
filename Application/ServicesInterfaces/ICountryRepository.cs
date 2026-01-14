using Fas7ny.Domain.Entities;

namespace Fas7ny.Application.ServicesInterfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllAsync();
    }

}
