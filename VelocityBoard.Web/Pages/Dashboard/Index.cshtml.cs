using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using VelocityBoard.Core.Entities;

namespace VelocityBoard.Web.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        public List<TaskItem> Tasks { get; set; } = new();

        public IndexModel(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync(string? search, string? status)
        {
            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                Response.Redirect("/Account/Login");
                return;
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(
                $"{_config["ApiSettings:BaseUrl"]}/api/tasks");

            if (!response.IsSuccessStatusCode) return;

            Tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>() ?? [];

            if (!string.IsNullOrEmpty(search))
                Tasks = Tasks.Where(t => t.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(status))
                Tasks = Tasks.Where(t => t.Status.ToString() == status).ToList();
        }
    }

}

