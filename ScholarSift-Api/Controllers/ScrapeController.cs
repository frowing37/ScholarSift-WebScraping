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
                
                //Alıntı sayısı ayrışıyor
                int index = documentQuotes.IndexOf(':');
                string numberofQuotes = documentQuotes.Substring(index + 1);
                
                //Makale yayım tarihi ayrışıyor
                int digitCount = 0;
                int firstDigitIndex = 0;
                string documentPublishDate = string.Empty;
                for (int i = 0; i < documentDescription.Length; i++)
                {
                    if(char.IsDigit(documentDescription[i]))
                    {
                        digitCount++;
                        if (digitCount == 1)
                            firstDigitIndex = i;
                        else if (digitCount == 4)
                            documentPublishDate = documentDescription.Substring(firstDigitIndex, 4);
                    }
                    else
                    {
                        digitCount = 0;
                    }
                }
                
                //Yazar adları ayrışıyor
                int firstWriterNameIndex = 0;
                int writerNameCounter = 0;
                int writerSpaceCounter = 0;
                string documentWritersName = " ";
                for (int i = 0; i < documentDescription.Length; i++)
                {
                    if (documentDescription[i] == ' ')
                    {
                        if(writerNameCounter != 0)
                            writerSpaceCounter++;
                        writerNameCounter++;
                    }
                    else if (documentDescription[i] == ',' || documentDescription[i] == '-')
                    {
                        documentWritersName = documentDescription.Substring(firstWriterNameIndex, i - firstWriterNameIndex).Trim() + ", ";
                        writerNameCounter = 0;
                        writerSpaceCounter = 0;
                        if(documentDescription[i] == '-')
                            break;
                    }
                    else if(char.IsUpper(documentDescription[i]))
                    {
                        if(writerNameCounter == 0)
                            firstWriterNameIndex = i;
                        writerNameCounter++;
                    }
                }
                
                //
                
                
                
                
                //Scrape nesnesi oluşturuluyor
                return new Scrape() {
                    DocumentLink = documentLink,
                    DocumentName = documentName,
                    DocumentDescription = documentDescription,
                    DocumentQuotes = numberofQuotes.Trim(),
                    DocumentFileLink = documentFileLink,
                    DocumentPublishDate = documentPublishDate,
                    DocumentWritersName = documentWritersName.TrimEnd()
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