using Fas7ny.Application.DTOs.Destination.Request;
using Fas7ny.Application.DTOs.Services;

namespace Fas7ny.Application.ServivesInterfaces
{
    public interface IMapboxSearchService
    {
        Task<MapboxGeocodeResponse> SearchAsync(MapboxGeocodeRequest query);
    }
}
