using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DarkTon_Web.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IMongoCollection<BsonDocument> _usersCollection;
        private readonly IMongoCollection<BsonDocument> _webtoonsCollection;
        private readonly IMongoCollection<BsonDocument> _episodesCollection;
        private readonly IMongoCollection<BsonDocument> _commentsCollection;

        public List<BsonDocument> Users { get; private set; } = new List<BsonDocument>();
        public List<BsonDocument> Webtoons { get; private set; } = new List<BsonDocument>();
        public List<BsonDocument> Episodes { get; private set; } = new List<BsonDocument>();
        public List<BsonDocument> Comments { get; private set; } = new List<BsonDocument>();

        public int TotalUsers { get; private set; }
        public int TotalWebtoons { get; private set; }
        public int TotalEpisodes { get; private set; }
        public int TotalComments { get; private set; }

        public AdminModel()
        {
            var client = new MongoClient("mongodb+srv://yucetalha8290:y5LUIKsd96PFsUmq@cluster0.lvofdz8.mongodb.net/");
            var database = client.GetDatabase("Web");
            _usersCollection = database.GetCollection<BsonDocument>("users");
            _webtoonsCollection = database.GetCollection<BsonDocument>("webtoons");
            _episodesCollection = database.GetCollection<BsonDocument>("episodes");
            _commentsCollection = database.GetCollection<BsonDocument>("comments");
        }

        public void OnGet()
        {
            Users = _usersCollection.Find(new BsonDocument()).ToList();
            Webtoons = _webtoonsCollection.Find(new BsonDocument()).ToList();
            Episodes = _episodesCollection.Find(new BsonDocument()).ToList();
            Comments = _commentsCollection.Find(new BsonDocument()).ToList();

            TotalUsers = Users.Count;
            TotalWebtoons = Webtoons.Count;
            TotalEpisodes = Episodes.Count;
            TotalComments = Comments.Count;
        }
    }
}
