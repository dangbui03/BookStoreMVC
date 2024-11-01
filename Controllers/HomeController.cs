using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookStoreMVC.Models;
using BookStoreMVC.Services;

namespace BookStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly FileUploadServices _fileuploadeservices;

        public HomeController(ILogger<HomeController> logger, FileUploadServices fileUploadServices)
        {
            _logger = logger;
            _fileuploadeservices = fileUploadServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index (IFormFile file)
        {
            try
            {
                if (await _fileuploadeservices.UploadFile(file))
                {
                    ViewBag.Message = "File Uploaded Successfully";
                } else {
                    ViewBag.Message = "File uploaded Failed";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "File uploaded Failed" + ex.Message;
                return View();
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename is not available");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "AllFiles", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

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
