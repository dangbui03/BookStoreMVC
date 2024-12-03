using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Data;
using BookStoreMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using GroupDocs.Viewer.Options;
using GroupDocs.Viewer;
using SrvnPortal.Helpers;

namespace BookStoreMVC.Controllers {
    public class BookController : Controller
    {
        private readonly BookDbContext _context;
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration, BookDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Details(BookViewModel vm)
        {
            vm.FileBooks = await _context.Files.ToListAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(BookViewModel vm, IFormFile file)
        {
            #region validate data
            if (file == null || vm == null)
            {
                return RedirectToAction("Details", "Book");
            }
            #endregion

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".docx", ".txt", ".pdf", ".xlxs", ".doc", ".xls", ".ppt", ".csv", ".pptx" };
            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
            {
                return RedirectToAction("Details", "Book");
            }

            //var fileName = DateTime.Now.ToString("yyyymmddhhmmss");
            //fileName = fileName + "_" + file.FileName;
            //var path = $"{_configuration.GetSection("FileManagement:SystemFileUpload").Value}"; // Path existing in appsettings.json
            var path = Path.Combine(Environment.CurrentDirectory, "AllFiles");

            var new_name = await FileMgr.UploadFile(file, path);

            var uploadFile = new FileBook
            {
                Name = new_name,
                Type = file.ContentType,
                Extension = fileExtension,
                Description = vm.Description,
                UploadBy = "Darkchan",
                UploadedDate = DateTime.Now,
                Path = path
            };

            await _context.AddAsync(uploadFile);
            await _context.SaveChangesAsync();

            //return View(vm);
            return RedirectToAction("Details", "Book");
        }

        public async Task<ActionResult> Download(string fileName, string type)
        {
            if (fileName == null)
            {
                return Content("filename is not availble");
            }

            string[] parts = fileName.Split('_');
            string extension = Path.GetExtension(fileName);
            string name = parts[0] + extension;

            //var path = $"{_configuration.GetSection("FileManagement:SystemFileUpload").Value}"; // Path existing in appsettings.json
            var path = Path.Combine(Environment.CurrentDirectory, "AllFiles");
            var filepath = Path.Combine(path, fileName);

            //var memory = new MemoryStream();
            //var stream = new FileStream(filepath, FileMode.Open);
            //await stream.CopyToAsync(memory);
            //memory.Position = 0;

            //return File(memory, type, name);
            // Mở file stream để đọc file
            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Tạo memory stream để lưu file
                var memory = new MemoryStream();
                await stream.CopyToAsync(memory);
                memory.Position = 0; // Reset position to the start of the stream

                // Trả về file với định dạng file hợp đồng
                return File(memory, type, name);
            }
        }

        public async Task<ActionResult> Delete(string fileName)
        {
            if (fileName == null)
            {
                return Content("filename is not availble");
            }

            string name = fileName.Substring(fileName.IndexOf('_') + 1);

            //var path = $"{_configuration.GetSection("FileManagement:SystemFileUpload").Value}"; // Path existing in appsettings.json
            var path = Path.Combine(Environment.CurrentDirectory, "AllFiles");
            var filepath = Path.Combine(path, fileName);

            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            var file = await _context.Files.FirstOrDefaultAsync(f => f.Name == fileName);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Book");
        }

        public async Task<ActionResult> viewFile(string fileName)
        {
            if (fileName == null)
            {
                return Content("filename is not availble");
            }

            string name = fileName.Substring(fileName.IndexOf('_') + 1);

            //var path = $"{_configuration.GetSection("FileManagement:SystemFileUpload").Value}"; // Path existing in appsettings.json
            var path2 = Path.Combine(Environment.CurrentDirectory, "AllFiles");
            var filepath = Path.Combine(path2, fileName);

            //Viewer viewer = new Viewer(filepath); //(path + "\\" + fileName);
            //PdfViewOptions options = new PdfViewOptions();
            //viewer.View(options);

            //var fileStream = new FileStream(path2 + "output.pdf", FileMode.Open, FileAccess.Read);
            //var fsResult = new FileStreamResult(fileStream, "application/pdf");
            //return fsResult;
            // Check if the file exists at the specified path
            if (!System.IO.File.Exists(filepath))
            {
                return Content("File not found.");
            }

            // Define a temporary path to store the PDF output
            string outputPath = Path.Combine(Environment.CurrentDirectory, "AllFiles", "temp_output.pdf");

            // Ensure the output directory exists
            string outputDirectory = Path.Combine(Environment.CurrentDirectory, "AllFiles");
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Initialize the viewer and render the file to PDF
            using (Viewer viewer = new Viewer(filepath))
            {
                PdfViewOptions options = new PdfViewOptions(outputPath);
                viewer.View(options);
            }

            // Serve the PDF file for preview
            var fileStream = new FileStream(outputPath, FileMode.Open, FileAccess.Read);
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
        }
    }
}
