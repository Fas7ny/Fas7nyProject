namespace Fas7ny.Application.DTOs.Payment.Response
{
    using System.Text.Json.Serialization;

    public class PaymobTokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;
    }


}
