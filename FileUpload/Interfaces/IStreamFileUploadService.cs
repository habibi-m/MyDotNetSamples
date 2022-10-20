using Microsoft.AspNetCore.WebUtilities;

namespace FileUpload.Interfaces
{
    public interface IStreamFileUploadService
    {
        Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
    }
}
