using System.Net;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ScholarSift_Data.Concrete;
using ScholarSift_Entity.Concrete;

namespace ScholarSift_Data.Services;

public class PDFService
{
    private static string localStoragePath = "/Users/frowing/Projects/ScholarSift/ScholarSift-UI/wwwroot/files";
    public async Task DownloadPdf(string url,string filename)
    {
        if(filename.Contains(' '))
            filename = filename.Replace(' ','_');
        
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, localStoragePath + "/" + filename + ".pdf");
        }
    }
}