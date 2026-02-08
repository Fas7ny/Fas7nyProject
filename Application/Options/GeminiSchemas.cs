namespace Fas7ny.Application.Services.AiService
{
    public static class GeminiSchemas
    {
        public static readonly object Chat = new
        {
            type = "object",
            properties = new
            {
                success = new { type = "boolean" },
                message = new { type = "string" },
                response = new { type = "string" }
            },
            required = new[] { "success", "response" }
        };

        public static readonly object GeneratePackage = new
        {
            type = "object",
            properties = new
            {
                success = new { type = "boolean" },
                data = new
                {
                    type = "object",
                    properties = new
                    {
                        packageName = new { type = "string" },
                        destinationCity = new { type = "string" },
                        durationDays = new { type = "integer" },
                        totalPrice = new { type = "number" },
                        pricePerPerson = new { type = "number" }
                    },
                    required = new[]
              {
                "packageName",
                "destinationCity",
                "durationDays",
                "totalPrice"
            }
                }
            },
            required = new[] { "success", "data" }
        };

        public static readonly object Recommendations = new
        {
            type = "object",
            properties = new
            {
                success = new { type = "boolean" },
                recommendations = new { type = "array" }
            },
            required = new[] { "success", "recommendations" }
        };

        public static readonly object UserBehaviorAnalysis = new
        {
            type = "object",
            properties = new
            {
                success = new { type = "boolean" },
                userId = new { type = "string" }
            },
            required = new[] { "success" }
        };
    }
}
