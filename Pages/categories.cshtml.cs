using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkTon_Web.Pages
{
   public class CategoriesModel : PageModel
{
    private readonly ILogger<CategoriesModel> _logger;
    private readonly WebtoonVeri _webtoonVeri;

    public List<Webtoon> Webtoons { get; private set; }

    public CategoriesModel(ILogger<CategoriesModel> logger)
    {
        _logger = logger;
        _webtoonVeri = new WebtoonVeri(); // WebtoonVeri sınıfını manuel olarak oluşturuyoruz.
    }

    public async Task OnGetAsync()
    {
        Webtoons = await _webtoonVeri.GetWebtoonsAsync();
    }
}

}
