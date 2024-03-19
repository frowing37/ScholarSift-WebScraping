using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ScholarSift_Data.Concrete;
using ScholarSift_Entity.Concrete;

namespace ScholarSift_Data.Services;

public class ArticleService
{
    private readonly IMongoCollection<Article> _collection;

    public ArticleService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _collection = mongoDb.GetCollection<Article>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<Article>> GetAsync() => await _collection.Find(_ => true).ToListAsync();
    public async Task<Article> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Article>> GetFilterListAsync(string keyword) =>
        await _collection.Find(x => x.Keywords == keyword).ToListAsync();
    public async Task CreateAsync(Article article) => await _collection.InsertOneAsync(article);
    public async Task UpdateAsync(Article article) => await _collection.ReplaceOneAsync(x => x.Id == article.Id, article);
    public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}