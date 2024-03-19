using ScholarSift_Entity.Concrete;

namespace ScholarSift_Entity.DTO;

public record ElasticArticleCreateDto(string Name, string Link,string Quates,string PublishDate,
    string Publisher, string Brief, string WritersName,string Keywords,string FileLink)
{
    public ElasticArticle CreateArticle()
    {
        return new ElasticArticle
        {
            Name = Name,
            Link = Link,
            Quates = Quates,
            PublishDate = PublishDate,
            Publisher = Publisher,
            Brief = Brief,
            WritersName = WritersName,
            Keywords = Keywords,
            FileLink = FileLink
        };
    }
}