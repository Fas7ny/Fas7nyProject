using Fas7ny.Application.DTOs.Geoapify.Response;
using Fas7ny.Application.Services.AlogailaSearche;
using Fas7ny.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Fas7ny.Infrastructure.ExternalApis
{
    public class GeoapifySearchService : IGeoapifySearchService
    {
        private readonly HttpClient _httpClient;
        private readonly GeoapifyOptions _options;

        public GeoapifySearchService(
            HttpClient httpClient,
            IOptions<GeoapifyOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<GeoapifyAutocompleteResponse> AutocompleteAsync(string text)
        {
            var url =
                $"{_options.BaseUrl}/geocode/autocomplete" +
                $"?text={Uri.EscapeDataString(text)}" +
                $"&apiKey={_options.ApiKey}";

            var response =
                await _httpClient.GetFromJsonAsync<GeoapifyAutocompleteResponse>(url);

            return response ?? new GeoapifyAutocompleteResponse();
        }
    }
}
