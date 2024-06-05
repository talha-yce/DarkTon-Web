using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DarkTon_Web.Pages
{
    public class Login : PageModel
    {
        private readonly ILogger<Login> _logger;

        public Login(ILogger<Login> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public IActionResult OnPost()
{
    var connectionString = "mongodb+srv://yucetalha8290:y5LUIKsd96PFsUmq@cluster0.lvofdz8.mongodb.net/";
    var databaseName = "Web";
    var collectionName = "users";

    var sorgu = new Sorgu(connectionString, databaseName, collectionName);
    bool isValidUser = sorgu.ValidateUser(Email, Password);

    if (isValidUser)
    {
        // Kullanıcı doğru giriş yaptı, oturum bilgilerini ayarla
        HttpContext.Session.SetString("IsLoggedIn", "true");
        HttpContext.Session.SetString("UserEmail", Email);

        if (Email == "admin@admin.com" && Password == "admin123")
        {
            return RedirectToPage("/Admin");
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }
    else
    {
        Message = "Email veya şifre yanlış. Lütfen tekrar deneyin.";
        return Page();
    }
}



    }
}
