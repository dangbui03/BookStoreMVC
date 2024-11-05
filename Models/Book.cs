namespace BookStoreMVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }

        // Thuộc tính liên kết với file Book
        public int FileDetailsId { get; set; }
        public FileBook FileDetails { get; set; } // Liên kết với model FileDetails
    }
}
