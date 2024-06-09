using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class Webtoon
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("comments")]
    public List<ObjectId> Comments { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("episodes")]
    public List<ObjectId> Episodes { get; set; }

    [BsonElement("likes")]
    public string Likes { get; set; }

    [BsonElement("saves")]
    public string Saves { get; set; }

    [BsonElement("coverImage")]
    public string CoverImage { get; set; }

    [BsonElement("genre")]
    public List<string> Genre { get; set; }
}
