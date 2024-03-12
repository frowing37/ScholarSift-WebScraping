using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Serializers;
using ScholarSift_Data.Services;
using ScholarSift_Entity.Concrete;
using ScholarSift_UI.Models;

namespace ScholarSift_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScrapeController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Scrape(MainSearchDto mainSearchDto)
    {
        string search = mainSearchDto.SearchText;
        
        if (search != string.Empty || search is not null)
        {
            search.Replace(' ', '+');
            var url = "https://scholar.google.com/scholar?hl=tr&as_sdt=0%2C5&q=" + search + "&oq=";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            
            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.gs_r.gs_or.gs_scl");
            
            var data = nodes.Select((node) => {
                
                var documentName = node.QuerySelector("div.gs_ri").QuerySelector("h3 a")?.InnerText ?? "Belirtilen Öğe bulunamadı";
                var documentLink = node.QuerySelector("div.gs_ri").QuerySelector("h3 a").GetAttributeValue("href", "heyyaaaa") ?? "Bulunamadı";
                var documentDescription = node.QuerySelector("div.gs_ri").QuerySelector("div.gs_a")?.InnerText ?? "Belirtilen Öğe Bulunamadı";
                var documentQuotes =
                    node.QuerySelector("div.gs_ri").QuerySelector("div.gs_fl.gs_flb")
                        .QuerySelector("a[href^=\"/scholar?cites=\"]").InnerText ?? "Belirtilen Öğe Bulunamadı";
                var documentFileLink = node.QuerySelector("div.gs_ggs.gs_fl div.gs_ggsd div.gs_or_ggsm a")?
                    .GetAttributeValue("href", string.Empty) ?? "Doküman linki bırakılmamış";
                
                int index = documentQuotes.IndexOf(':');
                string numberofQuotes = documentQuotes.Substring(index + 1);
                
                return new Scrape() {
                    DocumentLink = documentLink,
                    DocumentName = documentName,
                    DocumentDescription = documentDescription,
                    DocumentQuotes = numberofQuotes.Trim(),
                    DocumentFileLink = documentFileLink
                };
            });
            
            List<Scrape> scrapes = data.ToList();
            
            return Ok(scrapes);
        }
        else
        {
            return BadRequest();
        }
    }
}