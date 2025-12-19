using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace VelocityBoard.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        public RegisterModel(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        [BindProperty, Required]
        public string FullName { get; set; } = string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var client = _clientFactory.CreateClient();

            var response = await client.PostAsJsonAsync(
                $"{_config["ApiSettings:BaseUrl"]}/api/auth/register",
                new
                {
                    FullName,
                    Email,
                    Password
                });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Registration failed");
                return Page();
            }

            Message = "Registration successful! Please login.";
            ModelState.Clear();

            return Page();
        }
    }
}
