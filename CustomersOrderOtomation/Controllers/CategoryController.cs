using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }
        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await categoryService.GetAllCategories();
        }
    }
}
