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
    
    [BsonElement("Quates")]
    public string Quates { get; set; }
    
    [BsonElement("PublishDate")]
    public string PublishDate { get; set; }
    
    [BsonElement("WritersName")]
    public string WritersName { get; set; }
    
    [BsonElement("FileLink")]
    public string FileLink { get; set; }
    
    [BsonElement("File")]
    public BsonDocument Pdf { get; set; }
}