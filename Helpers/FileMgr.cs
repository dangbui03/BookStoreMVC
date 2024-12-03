using Microsoft.AspNetCore.Mvc;

namespace SrvnPortal.Helpers
{
    public class FileMgr
    {
        /// <summary>
        /// Uploads a file to the specified path.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <param name="uploadPath">The folder path to upload the file to.</param>
        /// <returns>The relative path of the uploaded file.</returns>
        public static async Task<string> UploadFile(IFormFile file, string fullPath)
        {
            // create name for file in directory
            var date_string = DateTime.Now.ToString("yyyyMMddHHmmss");
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var new_name = nameWithoutExtension + "_" + date_string + fileExtension;

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            var filepath = Path.Combine(fullPath, new_name);

            try
            {
                //save file to the folder
                using (var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new_name;
        }
    }
}
