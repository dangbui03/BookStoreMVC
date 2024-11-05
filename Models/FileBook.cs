using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models
{
    public class FileBook
    {
        public int Id { get; set; }       // ID duy nhất cho mỗi file

        [Display(Name = "File Name")]
        public string Name { get; set; }   // Tên file

        [Display(Name = "File Type")]
        public string Type { get; set; }   // Loại file

        [Display(Name = "File Extension")]
        public string Extension { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Upload By")]
        public string UploadBy { get; set; } // Người tải lên

        [Display(Name = "Path")]
        public string Path { get; set; }   // Đường dẫn lưu trữ file trên server

        [Display(Name = "Uploaded Date")]
        public DateTime UploadedDate { get; set; } // Ngày tải lên

        // Thuộc tính liên kết với Book
        //public Book Book { get; set; }
    }
}
