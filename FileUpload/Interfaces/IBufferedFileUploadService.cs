using FileUpload.Models;

namespace FileUpload.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<FileUploadResult> UploadFile(IFormFile file);
        Task<string> ReadImageToBase64String(string fileName);
    }
}
