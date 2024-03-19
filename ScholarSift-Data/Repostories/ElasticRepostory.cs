using Nest;
using ScholarSift_Entity.Concrete;

namespace ScholarSift_Data.Repostories;

public class ElasticRepostory
{
    private readonly ElasticClient _client;

    public ElasticRepostory(ElasticClient client) => _client = client;
    public async Task<ElasticArticle?> SaveAsync(ElasticArticle article)
    {
        var response = await _client.IndexAsync(article,x=>x.Index("articles"));
        
        if (!response.IsValid) return null;

        article.Id = response.Id;

        return article;
    }
}