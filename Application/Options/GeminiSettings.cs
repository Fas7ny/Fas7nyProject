namespace Fas7ny.Application.Options
{
    public class GeminiSettings
    {
        public string BaseUrl { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int TimeoutSeconds { get; set; }
    }


}
