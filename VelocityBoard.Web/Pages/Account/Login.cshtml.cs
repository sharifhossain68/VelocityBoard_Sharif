using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VelocityBoard.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        public LoginModel(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }
        public string? Error { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(
                $"{_config["ApiSettings:BaseUrl"]}/api/auth/login",
                new { Email, Password });

            if (!response.IsSuccessStatusCode)
            {
                Error = "Invalid login";
                return Page();
            }

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            HttpContext.Session.SetString("token", result!.Token);

            return RedirectToPage("/Dashboard/Index");
        }
    }

    public record TokenResponse(string Token);
}

