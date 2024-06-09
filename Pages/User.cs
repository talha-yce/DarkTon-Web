using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class User
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("biografi")]
    public string Biografi { get; set; }

     [BsonElement("profilimage")]
    public string ProfileImage { get; set; }

    [BsonElement("likedWebtoons")]
    public List<ObjectId> LikedWebtoons { get; set; }

    [BsonElement("savedWebtoons")]
    public List<ObjectId> SavedWebtoons { get; set; }
}
