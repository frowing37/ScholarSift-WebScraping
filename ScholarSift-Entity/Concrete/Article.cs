using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScholarSift_Entity.Concrete;

public class Article
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Name")]
    public string Name { get; set; }
    
    [BsonElement("Link")]
    public string Link { get; set; }
}