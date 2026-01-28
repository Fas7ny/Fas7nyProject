namespace Fas7ny.Application.DTOs.Ai.Request
{
    namespace Fas7ny.Application.DTOs.Mapbox.Request
    {
        public class MapboxGeocodeRequest
        {
            /// <summary>
            /// Search text (city, place, attraction, etc.)
            /// </summary>
            public string Query { get; set; } = string.Empty;

            /// <summary>
            /// Optional: Country filter (ISO 3166-1 alpha-2)
            /// Example: "eg", "fr"
            /// </summary>
            public string? Country { get; set; }

            /// <summary>
            /// Optional: Language for results
            /// Example: "en", "ar"
            /// </summary>
            public string? Language { get; set; }

            /// <summary>
            /// Optional: Limit results count
            /// Default: 5
            /// </summary>
            public int Limit { get; set; } = 5;
        }
    }

}
