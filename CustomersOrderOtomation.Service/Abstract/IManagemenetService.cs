using Microsoft.AspNetCore.Http;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IManagemenetService
    {
        Task<bool> CreateOrUpdateProductAsyncManagement(IFormCollection parameters);
    }
}
