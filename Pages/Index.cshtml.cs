using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkTon_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WebtoonVeri _webtoonVeri;

        public List<Webtoon> TrendingWebtoons { get; private set; }
        public List<Webtoon> NewWebtoons { get; private set; }
        public List<Webtoon> RecommendedWebtoons { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _webtoonVeri = new WebtoonVeri(); // Manuel olarak başlatma
        }

        public async Task OnGetAsync()
        {
            var webtoons = await _webtoonVeri.GetWebtoonsAsync();
            TrendingWebtoons = webtoons.Take(3).ToList(); // İlk 3'ü trend olarak ayarla
            NewWebtoons = webtoons.Skip(3).Take(3).ToList(); // Sonraki 3'ü yeni olarak ayarla
            RecommendedWebtoons = webtoons.Skip(6).Take(3).ToList(); // Sonraki 3'ü önerilen olarak ayarla
        }
    }
}
