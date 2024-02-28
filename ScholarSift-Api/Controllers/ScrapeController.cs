using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using ScholarSift_Entity.Concrete;

namespace ScholarSift_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScrapeController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Scrape(string search)
    {
        if (search != string.Empty || search is not null)
        {
            search.Replace(' ', '+');
            var url = "https://scholar.google.com/scholar?hl=tr&as_sdt=0%2C5&q=" + search + "&oq=";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.gs_r.gs_or.gs_scl").QuerySelectorAll("div.gs_ri");
            
            var data = nodes.Select((node) => {
                return new Scrape() {
                    DocumentName = node.QuerySelector("h3 a").InnerText,
                    DocumentLink = node.QuerySelector("h3 a").GetAttributeValue("href","heyyaaaaaaa") 
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