namespace Fas7ny.Application.Options
{
    public static class ImageUrlHelper
    {
        public static string BuildImageUrl(string baseUrl, string folder, string image)
        {
            if (string.IsNullOrWhiteSpace(image))
                return string.Empty;

            image = image.Replace($"{folder}/", "");

            return $"{baseUrl}/{folder}/{Uri.EscapeDataString(image)}";
        }
    }
}
