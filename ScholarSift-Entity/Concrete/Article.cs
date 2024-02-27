using MongoDB.Bson;

namespace ScholarSift_Entity.Concrete;

public class Article
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}