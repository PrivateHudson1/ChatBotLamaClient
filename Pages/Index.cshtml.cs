using ChatBotLlama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatBotLlama.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private static List<Message> _messages = new();

        [BindProperty]
        public string MessageText { get; set; }
        public List<Message> Messages { get; private set; } = new();


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Messages = _messages;
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(MessageText))
            {
                _messages.Add(new Message
                {
                    User = "Anon", // тут можно добавить авторизацию позже
                    Text = MessageText
                });
            }
            return RedirectToPage(); // обновляем страницу (чтобы отобразить новое сообщение)
        }
    }
}
