using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScholarSift_UI.Models;

public class ArticleDto
{
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string DocumentName { get; set; }
    
    public string DocumentLink { get; set; }
}