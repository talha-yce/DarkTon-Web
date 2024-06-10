using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DarkTon_Web.Pages
{
    public class Signup : PageModel
    {
        private readonly IMongoCollection<User> _userCollection;

        public Signup()
        {
            var mongoClient = new MongoClient("mongodb+srv://yucetalha8290:y5LUIKsd96PFsUmq@cluster0.lvofdz8.mongodb.net/");
            var database = mongoClient.GetDatabase("Web");
            _userCollection = database.GetCollection<User>("users");
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string email, string username, string password, string biografi, string profilimage)
        {
            // Create a new user object
            var newUser = new User
            {
                Email = email,
                Username = username,
                Password = password,
                Biografi = biografi,
                ProfileImage = profilimage,
                LikedWebtoons = new List<ObjectId>(), 
                SavedWebtoons = new List<ObjectId>()   
            };

            // Insert the new user into the database
            await _userCollection.InsertOneAsync(newUser);

            // Kullanıcı doğru giriş yaptı, oturum bilgilerini ayarla
            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("UserEmail", email);

            // Redirect to the index page
            return RedirectToPage("/Index");
        }
    }
}
