using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> OnPostUpdate()
        {
            if (!ModelState.IsValid) return Page();

            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Account/Login");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Use PUT for updating
            var response = await client.PutAsJsonAsync(
                $"{_config["ApiSettings:BaseUrl"]}/api/tasks/{Task.Id}", Task);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update task.");
                return Page();
            }

            return RedirectToPage("/Dashboard/Index");
        }

    }
}
