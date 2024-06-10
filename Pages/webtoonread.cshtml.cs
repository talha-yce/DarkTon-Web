using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkTon_Web.Pages
{
    public class webtoonreadModel : PageModel
    {
        private readonly ILogger<webtoonreadModel> _logger;
        private readonly WebtoonVeri _webtoonVeri;

        public Episode SelectedEpisode { get; set; }
        public Webtoon WebtoonDetails { get; set; }
        public List<Comment> EpisodeComments { get; set; }
        public List<User> CommentUsers { get; set; }
        public List<Episode> Episodes { get; set; }

        public webtoonreadModel(ILogger<webtoonreadModel> logger)
        {
            _logger = logger;
            _webtoonVeri = new WebtoonVeri();
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out ObjectId episodeId))
            {
                return NotFound();
            }

            SelectedEpisode = await _webtoonVeri.GetEpisodeById(episodeId);

            if (SelectedEpisode == null)
            {
                return NotFound();
            }

            WebtoonDetails = await _webtoonVeri.GetWebtoonById(SelectedEpisode.WebtoonId);
            EpisodeComments = await _webtoonVeri.GetCommentsByIds(SelectedEpisode.Comments);

            CommentUsers = new List<User>();
            foreach (var comment in EpisodeComments)
            {
                var user = await _webtoonVeri.GetUserById(comment.UserId);
                CommentUsers.Add(user);
            }

            Episodes = await _webtoonVeri.GetEpisodesByWebtoonId(WebtoonDetails.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string content)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out ObjectId episodeId))
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

            // Yeni yorumu ekleyin
            var commentId = await _webtoonVeri.AddComment(newComment);

            // İlgili bölümün comments listesine yorumun id'sini ekleyin
            await _webtoonVeri.AddCommentIdToEpisode(episodeId, commentId);

            return Page();
        }
    }
}
