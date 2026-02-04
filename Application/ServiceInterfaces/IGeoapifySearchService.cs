using Fas7ny.Application.DTOs.Geoapify.Response;

namespace Fas7ny.Application.ServivesInterfaces
{
    public interface GeoapifyPropertiesIGeoapifySearchService
    {
        public interface IGeoapifySearchService
        {
            Task<GeoapifyAutocompleteResponse> AutocompleteAsync(string text);
        }
    }
}