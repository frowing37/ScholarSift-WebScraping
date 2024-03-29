using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ScholarSift_Data.Services;
using ScholarSift_Entity.Concrete;
using ScholarSift_UI.Models;

namespace ScholarSift_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : Controller
{
    private readonly ArticleService _articleService;

    public ArticleController(ArticleService articleService) => _articleService = articleService;

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var existingDriver = await _articleService.GetAsync(id);

        if (existingDriver is null)
            return NotFound();

        return Ok(existingDriver);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allArticle = await _articleService.GetAsync();

        if (allArticle.Any())
            return Ok(allArticle);

        return NotFound();
    }

    [HttpGet("{keyword}")]
    public async Task<IActionResult> GetList(string keyword)
    {
        var articles = await _articleService.GetFilterListAsync(keyword);

        if (articles.Any())
            return Ok(articles);

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ArticleDto articleDto)
    {
        Article article = new Article();
        article.Link = articleDto.DocumentLink;
        article.Name = articleDto.DocumentName;
        article.Brief = articleDto.DocumentBrief;
        article.Pdf = articleDto.Pdf ?? null;
        article.Keywords = articleDto.DocumentKeyWords;
        article.Publisher = articleDto.DocumentPublisher;
        article.Quates = articleDto.DocumentQuotes;
        article.FileLink = articleDto.DocumentFileLink;
        article.PublishDate = articleDto.DocumentPublishDate;
        article.WritersName = articleDto.DocumentWritersName;
        
        await _articleService.CreateAsync(article);

        return CreatedAtAction(nameof(Get), new { id = articleDto.Id },articleDto);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Put(string Id, Article article)
    {
        var existingdriver = await _articleService.GetAsync(Id);

        if (existingdriver is null)
            return BadRequest();

        article.Id = existingdriver.Id;

        await _articleService.UpdateAsync(article);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string Id)
    {
        var existingarticle = await _articleService.GetAsync(Id);

        if (existingarticle is null)
            return BadRequest();

        await _articleService.RemoveAsync(Id);

        return NoContent();
    }
}