using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkTon_Web.Pages;

public class webtoondetails : PageModel
{
    private readonly ILogger<webtoondetails> _logger;

    public webtoondetails(ILogger<webtoondetails> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

