using Microsoft.AspNetCore.Mvc;
using ScholarSift_Data.Services;
using ScholarSift_UI.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace ScholarSift_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PDFController : Controller
{
    private readonly PDFService _pdfService;

    public PDFController(PDFService pdfService) => _pdfService = pdfService;
    
    [HttpPost]
    public IActionResult ReadPDF(FileNameDto fileNameDto)
    {
        string pdfContent;
        
        using (PdfReader pdfReader = new PdfReader(fileNameDto.Filename))
        {
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                
            }
        }
        
        return Ok();
    }
}