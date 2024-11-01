namespace BookStoreMVC.Models
{
    public class FileBook
    {
        public int Id { get; set; }       // ID duy nhất cho mỗi file
        public string Name { get; set; }   // Tên file
        public string Path { get; set; }   // Đường dẫn lưu trữ file trên server
        public DateTime UploadedDate { get; set; } // Ngày tải lên
    }
}
