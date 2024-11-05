using BookStoreMVC.Models;

namespace BookStoreMVC.ViewModels
{
    public class BookViewModel : FileBook
    {
        public byte[] FileData { get; set; }
        
        public List<FileBook> FileBooks { get; set; }
    }
}