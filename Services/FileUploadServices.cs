using BookStoreMVC.Interfaces;
using System.IO;
using BookStoreMVC.Data;
using BookStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Services
{
    public class FileUploadServices : IFileUpload
    {
        private readonly BookDbContext _context;

        public FileUploadServices(BookDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "AllFiles");

            try
            {
                // Check for invalid characters in the file name
                string sanitizedFileName = string.Join("_", file.FileName.Split(Path.GetInvalidFileNameChars()));

                // Create the directory if it doesn't exist
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Full file path for the new file
                string fullPath = Path.Combine(path, sanitizedFileName);

                // Copy the file content to the stream
                using (var filestream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                //var fileDetails = new FileBook
                //{
                //    Name = file.FileName,
                //    Path = path,
                //    UploadedDate = DateTime.Now
                //};

                //// Lưu thông tin file vào database
                //_context.Files.Add(fileDetails);
                //await _context.SaveChangesAsync();

                //// Liên kết FileDetails với Book
                //book.FileDetailsId = fileDetails.Id;
                //_context.Book.Add(book);
                //await _context.SaveChangesAsync();
  
                return true;
            }
            catch (IOException ioEx)
            {
                // Handle file IO specific errors here
                throw new Exception("File I/O error occurred while uploading the file.", ioEx);
            }
            catch (Exception ex)
            {
                // General exception handling
                throw new Exception("File could not be uploaded", ex);
            }
        }
    }
}
