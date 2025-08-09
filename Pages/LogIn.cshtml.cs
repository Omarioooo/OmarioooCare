using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OmarioooCare.DataAccess;

namespace OmarioooCare.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILoginService _loginService;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int ID { get; set; }

        public LoginModel(ILoginService loginService)
        {
            _loginService = loginService;
        }


        public IActionResult OnPost()
        {
            var success = _loginService.LoginCheck(Name, ID);

            if (success)
            {
                return RedirectToPage("/Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Login failed.");
            return Page();
        }
    }
}