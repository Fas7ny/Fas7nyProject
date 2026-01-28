using Fas7ny.Application.DTOs.Ai.Request.Fas7ny.Application.DTOs.Mapbox.Request;
using Fas7ny.Application.DTOs.Mapbox.Response;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Fas7ny.Infrastructure.ExternalApis
{
    public class MapboxSearchService : IMapboxSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly MapboxOptions _options;

        public MapboxSearchService(
            HttpClient httpClient,
            IOptions<MapboxOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<MapboxGeocodeResponse> SearchAsync(MapboxGeocodeRequest query)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                throw new ArgumentException("Search query cannot be empty.", nameof(query.Query));

            var url =
                $"{_options.BaseUrl}/geocoding/v5/mapbox.places/" +
                $"{Uri.EscapeDataString(query.Query)}.json" +
                $"?access_token={_options.AccessToken}" +
                $"&limit={query.Limit}";

            if (!string.IsNullOrEmpty(query.Country))
                url += $"&country={query.Country}";

            if (!string.IsNullOrEmpty(query.Language))
                url += $"&language={query.Language}";

            var response =
                await _httpClient.GetFromJsonAsync<MapboxGeocodeResponse>(url);

            return response ?? new MapboxGeocodeResponse();
        }

    }
}
