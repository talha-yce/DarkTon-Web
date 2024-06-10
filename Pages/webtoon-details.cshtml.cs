using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarkTon_Web.Models; // Bu satırı ekleyin

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

    public async Task<IActionResult> OnPostAsync(string id, string content)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out ObjectId webtoonId))
        {
            return NotFound();
        }

        if (HttpContext.Session.GetString("IsLoggedIn") != "true")
        {
            return Unauthorized();
        }

        var userEmail = HttpContext.Session.GetString("UserEmail");
        var userId = await _webtoonVeri.GetUserIdByEmail(userEmail);

        if (userId == null)
        {
            return NotFound("User not found.");
        }

        var newComment = new Comment
        {
            Content = content,
            Timestamp = DateTime.Now,
            UserId = userId.Value
        };

        // Yeni yorumu yorumlar koleksiyonuna ekleyin
        var commentId = await _webtoonVeri.AddComment(newComment);

        // Yorum ID'sini webtoon'un yorumlar listesine ekleyin
        await _webtoonVeri.AddCommentIdToWebtoon(webtoonId, commentId);

        return Redirect("/index");
    }
}
