using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Data;
using BookStoreMVC.ViewModels;

namespace BookStoreMVC.Controllers {
    public class BookController : Controller
    {
        private readonly BookDbContext _context;

        public BookController (BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(BookViewModel vm)
        {
            vm.FileBooks = new List<FileBook>();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Details(IFormFile file)//, Book book)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "AllFiles");

            try {
                bool flag = false;
                string sanitizedFileName = string.Join("_", file.FileName.Split(Path.GetInvalidFileNameChars()));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fullPath = Path.Combine(path, sanitizedFileName);

                using (var filestream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }
                flag = true;
                if (flag)
                {
                    ViewBag.Message = "File Uploaded Successfully";
                } else {
                    ViewBag.Message = "File uploaded Failed";
                }

                var fileDetails = new FileBook
                {
                    Name = file.FileName,
                    Path = path,
                    UploadedDate = DateTime.Now
                };

                _context.Files.Add(fileDetails);
                await _context.SaveChangesAsync();

                // book.FileDetailsId = fileDetails.Id;
                // _context.Book.Add(book);
                // await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Book");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "File uploaded Failed" + ex.Message;
                return View();
            }
        }
    }
}
