using FileUpload.Interfaces;
using FileUpload.Models;

namespace FileUpload.Services
{
    public class BufferedFileUploadService : IBufferedFileUploadService
    {
        readonly IWebHostEnvironment _appEnvironment;

        public BufferedFileUploadService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<FileUploadResult> UploadFile(IFormFile file)
        {
            try
            {
                var uploadResult = new FileUploadResult();
                uploadResult.Result = false;

                if (file != null && file.Length > 0)
                {
                    var path = Path.GetFullPath(Path.Combine(_appEnvironment.WebRootPath, "UploadedFiles"));
                    var fileName = file.FileName;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    while (File.Exists(Path.Combine(path, fileName)))
                    {
                        fileName = $"{DateTime.Now.ToFileTime()}-{Path.GetRandomFileName()}{Path.GetExtension(fileName)}";
                    }

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    uploadResult.Result = true;
                    uploadResult.SavedFileName = fileName;
                }
                return uploadResult;
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        public async Task<string> ReadImageToBase64String(string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    var path = Path.GetFullPath(Path.Combine(_appEnvironment.WebRootPath, "UploadedFiles"));

                    if (!Directory.Exists(path))
                        return null;

                    var fileBytes = await File.ReadAllBytesAsync(Path.Combine(path, fileName));
                    
                    return $"data:image/png;base64,{Convert.ToBase64String(fileBytes)}";
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
