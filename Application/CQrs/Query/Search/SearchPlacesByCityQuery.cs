using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.TouristPlace.Response;

namespace Fas7ny.Application.CQrs.Query.Search
{
    public class SearchPlacesByCityQuery : IQuery<Result<List<TouristPlaceResponseDTO>>>
    {
        public Guid CityId { get; set; }
    }
}
