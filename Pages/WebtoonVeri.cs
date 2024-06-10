using MongoDB.Bson;
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

    public IMongoCollection<Episode> GetEpisodeCollection()
    {
        return _episodeCollection;
    }

    public IMongoCollection<Comment> GetCommentCollection()
    {
        return _commentCollection;
    }

    public async Task AddCommentToEpisode(ObjectId episodeId, Comment newComment)
    {
        var filter = Builders<Episode>.Filter.Eq(e => e.Id, episodeId);
        var update = Builders<Episode>.Update.Push(e => e.Comments, newComment.Id);
        await _episodeCollection.UpdateOneAsync(filter, update);
    }

    public async Task<Episode> GetEpisodeById(ObjectId episodeId)
    {
        return await _episodeCollection.Find(e => e.Id == episodeId).FirstOrDefaultAsync();
    }

    public async Task<ObjectId> AddComment(Comment newComment)
    {
        await _commentCollection.InsertOneAsync(newComment);
        return newComment.Id;
    }

    public async Task AddCommentIdToEpisode(ObjectId episodeId, ObjectId commentId)
    {
        var filter = Builders<Episode>.Filter.Eq(e => e.Id, episodeId);
        var update = Builders<Episode>.Update.Push(e => e.Comments, commentId);
        await _episodeCollection.UpdateOneAsync(filter, update);
    }

    public async Task<ObjectId?> GetUserIdByEmail(string email)
{
    var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
    return user?.Id;
}
public async Task AddCommentIdToWebtoon(ObjectId webtoonId, ObjectId commentId)
{
    var filter = Builders<Webtoon>.Filter.Eq(w => w.Id, webtoonId);
    var update = Builders<Webtoon>.Update.Push(w => w.Comments, commentId);
    await _webtoonCollection.UpdateOneAsync(filter, update);
}


}