using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookStoreMVC.Models;

namespace BookStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        // private readonly FileUploadServices _fileuploadeservices;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // [HttpPost]
        // public async Task<ActionResult> Index (IFormFile file)
        // {
        //     string path = Path.Combine(Environment.CurrentDirectory, "AllFiles");
        //     try
        //     {
        //         bool flag = true;
        //         string sanitizedFileName = string.Join("_", file.FileName.Split(Path.GetInvalidFileNameChars()));

        //         if (!Directory.Exists(path))
        //         {
        //             Directory.CreateDirectory(path);
        //         }

        //         // Full file path for the new file
        //         string fullPath = Path.Combine(path, sanitizedFileName);

        //         // Copy the file content to the stream
        //         using (var filestream = new FileStream(fullPath, FileMode.Create))
        //         {
        //             await file.CopyToAsync(filestream);
        //         }

        //         if (await _fileuploadeservices.UploadFile(file))
        //         {
        //             ViewBag.Message = "File Uploaded Successfully";
        //         } else {
        //             ViewBag.Message = "File uploaded Failed";
        //         }
        //         return View();
        //     }
        //     catch (Exception ex)
        //     {
        //         ViewBag.Message = "File uploaded Failed" + ex.Message;
        //         return View();
        //     }
        // }

        // private string GetContentType(string path)
        // {
        //     var types = GetMimeTypes();
        //     var ext = Path.GetExtension(path).ToLowerInvariant();
        //     return types[ext];
        // }

        // private Dictionary<string, string> GetMimeTypes()
        // {
        //     return new Dictionary<string, string>
        //     {
        //         {".txt", "text/plain"},
        //         {".pdf", "application/pdf"},
        //         {".doc", "application/vnd.ms-word"},
        //         {".docx", "application/vnd.ms-word"},
        //         {".xls", "application/vnd.ms-excel"},
        //         {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        //         {".png", "image/png"},
        //         {".jpg", "image/jpeg"},
        //         {".jpeg", "image/jpeg"},
        //         {".gif", "image/gif"},
        //         {".csv", "text/csv"}
        //     };
        // }

        // public async Task<IActionResult> Download(string filename)
        // {
        //     if (filename == null)
        //         return Content("filename is not available");

        //     var path = Path.Combine(Directory.GetCurrentDirectory(), "AllFiles", filename);

        //     var memory = new MemoryStream();
        //     using (var stream = new FileStream(path, FileMode.Open))
        //     {
        //         await stream.CopyToAsync(memory);
        //     }
        //     memory.Position = 0;
        //     return File(memory, GetContentType(path), Path.GetFileName(path));
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
