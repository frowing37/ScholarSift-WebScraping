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
    public IActionResult InstallPDF(PdfDto pdf)
    {
        _pdfService.DownloadPdf(pdf.FileUrl,pdf.FileName);
        
        return Ok();
    }
}