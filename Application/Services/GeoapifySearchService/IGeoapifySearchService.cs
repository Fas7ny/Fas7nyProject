using Fas7ny.Application.DTOs.Geoapify.Response;

namespace Fas7ny.Application.Services.AlogailaSearche
{
    public interface IGeoapifySearchService
    {
        public interface IGeoapifySearchService
        {
            Task<GeoapifyAutocompleteResponse> AutocompleteAsync(string text);
        }
    }
}