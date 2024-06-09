using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WebtoonVeri
{
    private readonly IMongoCollection<Webtoon> _webtoonCollection;
    private readonly IMongoCollection<Episode> _episodeCollection;
    private readonly IMongoCollection<Comment> _commentCollection;
    private readonly IMongoCollection<User> _userCollection;

    public WebtoonVeri()
    {
        var connectionString = "mongodb+srv://yucetalha8290:y5LUIKsd96PFsUmq@cluster0.lvofdz8.mongodb.net/";
        var databaseName = "Web";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _webtoonCollection = database.GetCollection<Webtoon>("webtoons");
        _episodeCollection = database.GetCollection<Episode>("episodes");
        _commentCollection = database.GetCollection<Comment>("comments");
        _userCollection = database.GetCollection<User>("users");
    }

    public async Task<Webtoon> GetWebtoonById(ObjectId id)
    {
        return await _webtoonCollection.Find(webtoon => webtoon.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Episode>> GetEpisodesByWebtoonId(ObjectId webtoonId)
    {
        return await _episodeCollection.Find(e => e.WebtoonId == webtoonId).ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsByIds(List<ObjectId> commentIds)
    {
        return await _commentCollection.Find(c => commentIds.Contains(c.Id)).ToListAsync();
    }

    public async Task<User> GetUserById(ObjectId userId)
    {
        return await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();
    }
     public async Task<List<Webtoon>> GetWebtoonsAsync()
    {
        return await _webtoonCollection.Find(new BsonDocument()).ToListAsync();
    }
}
