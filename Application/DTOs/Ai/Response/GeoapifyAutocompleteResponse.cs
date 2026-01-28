namespace Fas7ny.Application.DTOs.Geoapify.Response
{
    public class GeoapifyAutocompleteResponse
    {
        public List<GeoapifyFeature> Features { get; set; } = new();
    }

    public class GeoapifyFeature
    {
        public GeoapifyProperties Properties { get; set; } = new();
    }

    public class GeoapifyProperties
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Formatted { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}

