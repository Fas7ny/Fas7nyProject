namespace Fas7ny.Application.DTOs.Destination.Response
{
    public class MapboxGeocodeResponse
    {
        public List<MapboxFeature> Features { get; set; } = new();
    }
    public class MapboxFeature
    {
        public string PlaceName { get; set; }
        public List<double> Center { get; set; } // [longitude, latitude]
        public string PlaceType { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }


}
