using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using VelocityBoard.Core.Entities;

namespace VelocityBoard.Web.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        [BindProperty]
        public TaskItem Task { get; set; } = new();

        public CreateModel(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Account/Login");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            await client.PostAsJsonAsync(
                $"{_config["ApiSettings:BaseUrl"]}/api/tasks", Task);

            return RedirectToPage("/Dashboard/Index");
        }
    }
}
