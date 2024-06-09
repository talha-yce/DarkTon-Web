using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Comment
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("content")]
    public string Content { get; set; }

    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("userId")]
    public ObjectId UserId { get; set; }
}
