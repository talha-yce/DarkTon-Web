using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkTon_Web.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IMongoCollection<BsonDocument> _userCollection;
        private readonly IMongoCollection<BsonDocument> _webtoonsCollection;

        public ProfileModel()
        {
            var connectionString = "mongodb+srv://yucetalha8290:y5LUIKsd96PFsUmq@cluster0.lvofdz8.mongodb.net/";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Web");
            _userCollection = database.GetCollection<BsonDocument>("users");
            _webtoonsCollection = database.GetCollection<BsonDocument>("webtoons");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/login");
            }

            var filter = Builders<BsonDocument>.Filter.Eq("email", userEmail);
            var user = await _userCollection.Find(filter).FirstOrDefaultAsync();

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            Username = user.GetValue("username").AsString;
            Email = user.GetValue("email").AsString;
            Biografi = user.GetValue("biografi").AsString;
            ProfilImage = user.GetValue("profilimage").AsString;
            SavedWebtoons = user.GetValue("savedWebtoons").AsBsonArray.Select(x => x.ToString()).ToArray();

            var webtoonDetails = new List<WebtoonDetails>();

            foreach (var webtoonId in SavedWebtoons)
            {
                var webtoonFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(webtoonId));
                var webtoonDocument = await _webtoonsCollection.Find(webtoonFilter).FirstOrDefaultAsync();

                if (webtoonDocument != null)
                {
                    var webtoon = new WebtoonDetails
                    {
                        Title = webtoonDocument.GetValue("title").AsString,
                        CoverImage = webtoonDocument.GetValue("coverImage").AsString,
                        Genres = webtoonDocument.GetValue("genre").AsBsonArray.Select(x => x.ToString()).ToArray(),
                        Likes = webtoonDocument.GetValue("likes").AsString, // Use as string
                        Saves = webtoonDocument.GetValue("saves").AsString  // Use as string
                    };
                    webtoonDetails.Add(webtoon);
                }
            }

            SavedWebtoonsDetails = webtoonDetails;

            return Page();
        }

        public class WebtoonDetails
        {
            public string Title { get; set; }
            public string CoverImage { get; set; }
            public string[] Genres { get; set; }
            public string Likes { get; set; } // Use string
            public string Saves { get; set; } // Use string
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Biografi { get; set; }
        public string ProfilImage { get; set; }
        public string[] SavedWebtoons { get; set; }
        public List<WebtoonDetails> SavedWebtoonsDetails { get; set; }
    }
}
