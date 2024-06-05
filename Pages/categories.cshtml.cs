using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkTon_Web.Pages;

public class Categories : PageModel
{
    private readonly ILogger<Categories> _logger;

    public Categories(ILogger<Categories> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

