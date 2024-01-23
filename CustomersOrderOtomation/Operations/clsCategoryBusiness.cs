using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.Service.Concrete;
using CustomersOrderOtomation.ViewModel.Category;

namespace CustomersOrderOtomation.Operations
{
    public class clsCategoryBusiness
    {
        private readonly ICategoryService categoryService;

        public clsCategoryBusiness(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await categoryService.GetAllCategories();
        }
    }
}
