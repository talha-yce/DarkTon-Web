using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkTon_Web.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Oturum bilgilerini temizle
            HttpContext.Session.Clear();
            // Ana sayfaya yönlendir
            return RedirectToPage("/Index");
        }
    }
}
