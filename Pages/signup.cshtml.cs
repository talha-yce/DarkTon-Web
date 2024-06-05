using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkTon_Web.Pages;

public class Signup : PageModel
{
    private readonly ILogger<Signup> _logger;

    public Signup(ILogger<Signup> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

