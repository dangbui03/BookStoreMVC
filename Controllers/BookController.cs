using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Data;
using BookStoreMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Controllers {
    public class BookController : Controller
    {
        private readonly BookDbContext _context;
        private readonly IConfiguration _configuration;

        public BookController (IConfiguration configuration, BookDbContext context)
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
            if (file == null || vm == null)
            {
                return RedirectToAction("Details", "Book");
            }

            var fileName = DateTime.Now.ToString("yyyymmddhhmmss");
            fileName = fileName + "_" + file.FileName;
            var path = $"{_configuration.GetSection("FileManagement:SystemFileUpload").Value}";
            var filepath = Path.Combine(path, fileName);
            
            var fileExtension = Path.GetExtension(fileName);
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            //save file to the folder
            var stream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(stream);

            var uploadFile = new FileBook
            {
                Name = fileName,
                Type = file.ContentType,
                Extension = fileExtension,
                Description = vm.Description,
                UploadBy = "Darkchan",
                UploadedDate = DateTime.Now,
                Path = filepath
            };

            await _context.AddAsync(uploadFile);
            await _context.SaveChangesAsync();

            //return View(vm);
            return RedirectToAction("Details", "Book");
        }


        //        [HttpPost]
        //        public async Task<IActionResult> Details(IFormFile file)//, Book book)
        //        {
        //            string path = Path.Combine(Environment.CurrentDirectory, "AllFiles");

            //            try {
            //                bool flag = false;
            //                string sanitizedFileName = string.Join("_", file.FileName.Split(Path.GetInvalidFileNameChars()));

            //                if (!Directory.Exists(path))
            //                {
            //                    Directory.CreateDirectory(path);
            //                }

            //                string fullPath = Path.Combine(path, sanitizedFileName);

            //                using (var filestream = new FileStream(fullPath, FileMode.Create))
            //                {
            //                    await file.CopyToAsync(filestream);
            //                }
            //                flag = true;
            //                if (flag)
            //                {
            //                    ViewBag.Message = "File Uploaded Successfully";
            //                } else {
            //                    ViewBag.Message = "File uploaded Failed";
            //                }

            //                var fileDetails = new FileBook
            //                {
            //                    Name = file.FileName,
            //                    Path = path,
            //                    UploadedDate = DateTime.Now
            //                };

            //                _context.Files.Add(fileDetails);
            //                await _context.SaveChangesAsync();

            //                // book.FileDetailsId = fileDetails.Id;
            //                // _context.Book.Add(book);
            //                // await _context.SaveChangesAsync();

            //                return RedirectToAction("Details", "Book");
            //            }
            //            catch (Exception ex)
            //            {
            //                ViewBag.Message = "File uploaded Failed" + ex.Message;
            //                return View();
            //            }
            //        }
    }
}
