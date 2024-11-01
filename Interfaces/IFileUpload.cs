namespace BookStoreMVC.Interfaces
{
    public interface IFileUpload
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
