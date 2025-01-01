using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.ViewModel.Category;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategories();
        Task<CategoryDetailViewModel> GetCategoryById(int id);
        Task UpdateCategoryAsync(int id, CategoryDto updateResource);
        Task AddCategoryAsync(CategoryDto updateResource);
        Task SafeDeleteCategorytAsync(int id);
    }
}
