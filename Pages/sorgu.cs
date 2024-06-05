using MongoDB.Bson;
using MongoDB.Driver;

public class Sorgu
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public Sorgu(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<BsonDocument>(collectionName);
    }

   public bool ValidateUser(string? email, string? password)
    {
        var filter = Builders<BsonDocument>.Filter.And(
            Builders<BsonDocument>.Filter.Eq("email", email),
            Builders<BsonDocument>.Filter.Eq("password", password)
        );
        
        var user = _collection.Find(filter).FirstOrDefault();
        return user != null;
    }
}
