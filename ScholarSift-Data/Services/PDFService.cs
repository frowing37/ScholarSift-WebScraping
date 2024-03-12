using System.Net;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ScholarSift_Data.Concrete;
using ScholarSift_Entity.Concrete;

namespace ScholarSift_Data.Services;

public class PDFService
{
    private static string localStoragePath = "~/files/";
    
    public static void DownloadPdf(string url)
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, localStoragePath);
        }
    }
}