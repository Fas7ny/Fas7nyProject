using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.TouristPlace.Response;

namespace Fas7ny.Application.CQrs.Query.Search
{
    public class SearchPlacesQuery : IQuery<Result<List<TouristPlaceResponseDTO>>>
    {
        public string SearchTerm { get; set; }
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
    }
}
