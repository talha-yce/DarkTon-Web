using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

public class Episode
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("webtoonId")]
    public ObjectId WebtoonId { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("comments")]
    public List<ObjectId> Comments { get; set; } = new List<ObjectId>();

    [BsonElement("releaseDate")]
    public DateTime ReleaseDate { get; set; }

    [BsonElement("images")]
    public List<string> Images { get; set; } // images alanını ekledik
}
