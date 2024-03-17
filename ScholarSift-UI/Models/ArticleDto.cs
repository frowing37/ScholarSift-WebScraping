using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScholarSift_UI.Models;

public class ArticleDto
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string DocumentName { get; set; } 
    public string DocumentLink { get; set; }
    
    public string DocumentQuotes { get; set; }
    
    public string DocumentFileLink { get; set; }
    
    public string DocumentPublishDate { get; set; }
    
    public string DocumentWritersName { get; set; }
    
    public string DocumentPublisher { get; set; }
    
    public string DocumentBrief { get; set; }
    
    public string DocumentKeyWords { get; set; }
    
    public byte[] Pdf { get; set; }
}