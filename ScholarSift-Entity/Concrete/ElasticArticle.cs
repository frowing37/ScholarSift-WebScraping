using Nest;
using ScholarSift_Entity.DTO;

namespace ScholarSift_Entity.Concrete;

public class ElasticArticle
{
    [PropertyName("_id")]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string Quates { get; set; }
    public string PublishDate { get; set; }
    public string Publisher { get; set; }
    public string Brief { get; set; }
    public string WritersName { get; set; }
    public string Keywords { get; set; }
    public string FileLink { get; set; }
    
    //public byte[] Pdf { get; set; }

    public ElasticArticleDto CreateDto()
    {
        return new ElasticArticleDto(Id, Name, Link, Quates, PublishDate, Publisher, Brief, WritersName, Keywords,
            FileLink);
    }
}