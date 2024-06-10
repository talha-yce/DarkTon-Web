using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EpisodeDataService
{
    private readonly IMongoCollection<Episode> _episodes;
    private readonly IMongoCollection<Webtoon> _webtoons;

    public EpisodeDataService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _episodes = database.GetCollection<Episode>("Episodes");
        _webtoons = database.GetCollection<Webtoon>("Webtoons");
    }

    public async Task<Episode> GetEpisodeByIdAsync(ObjectId episodeId)
    {
        return await _episodes.Find(e => e.Id == episodeId).FirstOrDefaultAsync();
    }

    public async Task<Webtoon> GetWebtoonByIdAsync(ObjectId webtoonId)
    {
        return await _webtoons.Find(w => w.Id == webtoonId).FirstOrDefaultAsync();
    }
}
