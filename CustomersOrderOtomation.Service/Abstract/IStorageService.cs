using Microsoft.AspNetCore.Http;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string blobName);
    }
}
