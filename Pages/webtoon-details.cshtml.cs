using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

public class webtoondetails : PageModel
{
    private readonly ILogger<webtoondetails> _logger;
    private readonly WebtoonVeri _webtoonVeri;

    public Webtoon SelectedWebtoon { get; private set; }
    public List<Episode> Episodes { get; private set; }
    public List<CommentViewModel> Comments { get; private set; }

    public webtoondetails(ILogger<webtoondetails> logger)
    {
        _logger = logger;
        _webtoonVeri = new WebtoonVeri();
    }

    public async Task OnGetAsync(string id)
    {
        var objectId = new ObjectId(id);
        SelectedWebtoon = await _webtoonVeri.GetWebtoonById(objectId);
        Episodes = await _webtoonVeri.GetEpisodesByWebtoonId(objectId);
        var comments = await _webtoonVeri.GetCommentsByIds(SelectedWebtoon.Comments);
        Comments = new List<CommentViewModel>();

        foreach (var comment in comments)
        {
            var user = await _webtoonVeri.GetUserById(comment.UserId);
            Comments.Add(new CommentViewModel
            {
                ProfileImage = user.ProfileImage,
                Username = user.Username,
                Timestamp = comment.Timestamp,
                Content = comment.Content
            });
        }
    }
}

public class CommentViewModel
{
    public string ProfileImage { get; set; }
    public string Username { get; set; }
    public DateTime Timestamp { get; set; }
    public string Content { get; set; }
}
