namespace Fas7ny.Application.Options
{
    public class OpenRouterSettings
    {
        public string BaseUrl { get; set; } = default!;
        public string ApiKey { get; set; } = default!;
        public string DefaultModel { get; set; } = default!;
        public int TimeoutSeconds { get; set; }
    }

}
