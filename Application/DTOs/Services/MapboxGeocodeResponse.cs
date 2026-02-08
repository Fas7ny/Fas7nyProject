using System.Text.Json.Serialization;

namespace Fas7ny.Application.DTOs.Services
{
    public class MapboxGeocodeResponse
    {
        [JsonPropertyName("features")]
        public List<MapboxFeature> Features { get; set; } = new();
    }

    public class MapboxFeature
    {
        [JsonPropertyName("place_name")]
        public string PlaceName { get; set; }

        [JsonPropertyName("center")]
        public double[] Center { get; set; }
        public object Text { get; set; }
        public object PlaceType { get; set; }
    }
}
