using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkTon_Web.Pages;

public class webtoonread : PageModel
{
    private readonly ILogger<webtoonread> _logger;

    public webtoonread(ILogger<webtoonread> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

