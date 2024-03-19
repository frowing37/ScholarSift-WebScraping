using Microsoft.AspNetCore.Mvc;
using Nest;
using ScholarSift_Entity.DTO;
using ScholarSift_Data.Services;
using ScholarSift_Entity.Concrete;

namespace ScholarSift_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElasticController : Controller
{
    private readonly ElasticService _elasticService;

    public ElasticController(ElasticService elasticService) => _elasticService = elasticService;

    [HttpPost]
    public async Task<IActionResult> Save(ElasticArticleCreateDto request)
    {
        return Ok(await _elasticService.SaveAsync(request));
    }

    [HttpGet]
    public async Task<IActionResult> Indexes()
    {
        ElasticClient a = new ElasticClient();
        var indices = await a.Cat.IndicesAsync();
        List<string> namesIndex = new List<string>();
        foreach(var index in indices.Records)
        {
            namesIndex.Add(index.Index);
        }

        return Ok(namesIndex.Count);
    }
}