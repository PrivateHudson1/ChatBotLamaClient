using ChatBotLlama.Models;
using ChatBotLlamaClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ChatBotLlama.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiSettings _apiSettings;
        private readonly ILogger<IndexModel> _logger;
        private static List<Message> _messages = new();

        [BindProperty]
        public string MessageText { get; set; }
        public List<Message> Messages { get; private set; } = new();


        public IndexModel(ILogger<IndexModel> logger, IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
            _logger = logger;
        }

        public void OnGet()
        {
            Messages = _messages;
            ViewData["ApiBaseUrl"] = _apiSettings.BaseUrl;
            ViewData["ChatHubUrl"] = _apiSettings.ChatHubUrl;
            ViewData["Timeout"] = _apiSettings.Timeout;
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(MessageText))
            {
                _messages.Add(new Message
                {
                    User = "Anon", 
                    Text = MessageText
                });
            }
            return RedirectToPage(); 
        }
    }
}
