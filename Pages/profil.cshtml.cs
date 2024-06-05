using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace DarkTon_Web.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IMongoCollection<BsonDocument> _userCollection;

        public ProfileModel()
        {
            var connectionString = "mongodb+srv://yucetalha8290:y5LUIKsd96PFsUmq@cluster0.lvofdz8.mongodb.net/"; // MongoDB bağlantı dizesi
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Web"); // Veritabanı adı
            _userCollection = database.GetCollection<BsonDocument>("users");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // Kullanıcı girişi yapmamışsa giriş sayfasına yönlendir
                return RedirectToPage("/login");
            }

            var filter = Builders<BsonDocument>.Filter.Eq("email", userEmail);
            var user = await _userCollection.Find(filter).FirstOrDefaultAsync();

            if (user == null)
            {
                // Kullanıcı bulunamadıysa hata sayfasına yönlendir
                return RedirectToPage("/Error");
            }

            // Kullanıcı bilgilerini model özelliklerine aktar
            Username = user.GetValue("username").AsString;
            Email = user.GetValue("email").AsString;
            Biografi = user.GetValue("biografi").AsString;
            ProfilImage = user.GetValue("profilimage").AsString;
            LikedWebtoons = user.GetValue("likedWebtoons").AsBsonArray.Select(x => x.ToString()).ToArray();
            SavedWebtoons = user.GetValue("savedWebtoons").AsBsonArray.Select(x => x.ToString()).ToArray();

            return Page();
        }

        // Model özellikleri
        public string Username { get; set; }
        public string Email { get; set; }
        public string Biografi { get; set; }
        public string ProfilImage { get; set; }
        public string[] LikedWebtoons { get; set; }
        public string[] SavedWebtoons { get; set; }
    }
}
