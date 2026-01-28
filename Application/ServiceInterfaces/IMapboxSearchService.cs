using Fas7ny.Application.DTOs.Ai.Request.Fas7ny.Application.DTOs.Mapbox.Request;
using Fas7ny.Application.DTOs.Mapbox.Response;

namespace Fas7ny.Application.ServivesInterfaces
{
    public interface IMapboxSearchService
    {
        Task<MapboxGeocodeResponse> SearchAsync(MapboxGeocodeRequest query);
    }
}
