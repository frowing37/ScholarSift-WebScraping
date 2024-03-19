using Elasticsearch.Net;

namespace ScholarSift_Api.Extensions;
using Nest;

public static class Elasticsearch
{
    public static void AddElastic(this IServiceCollection services,IConfiguration configuration)
    {
        var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
        var settings = new ConnectionSettings(pool);
        settings.BasicAuthentication("elastic", "changeme");
        settings.DefaultIndex("articles");
        var client = new ElasticClient(settings);
        services.AddSingleton(client);
    }
}