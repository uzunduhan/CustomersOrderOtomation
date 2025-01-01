using CustomersOrderOtomation.ViewModel.Category;
using CustomersOrderOtomation.ViewModel.Product;
using Microsoft.AspNetCore.Http;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IManagemenetService
    {
        Task<bool> CreateOrUpdateProductAsyncManagement(IFormCollection parameters);
        Task<List<ProductViewModelManagement>> GetAllProductsForManagement();
        Task<ProductViewModelManagement> GetSingleProductByIdForManagement(int id);
        Task<List<CategoryViewModelManagement>> GetAllCategoriesForManagement();
        Task<CategoryViewModelManagement> GetSingleCategoryByIdForManagement(int id);
        Task<bool> CreateOrUpdateCategoryAsyncManagement(IFormCollection parameters);
    }
}
