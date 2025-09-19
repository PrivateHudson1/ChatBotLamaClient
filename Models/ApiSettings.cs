using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ChatBotLlamaClient.Models
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public string ChatHubUrl { get; set; }
        public int Timeout { get; set; }
    }
}
