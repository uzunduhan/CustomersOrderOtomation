using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Category;
using CustomersOrderOtomation.ViewModel.Product;
using CustomersOrderOtomation.ViewModel.ShopList;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class ManagementController : Controller
    {
        public ManagementController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageProduct([FromServices] IManagemenetService managementService)
        {
            var products = await managementService.GetAllProductsForManagement();

            return View(products);
        }

        public async Task<IActionResult> ManageCategory([FromServices] IManagemenetService managementService)
        {
            var categories = await managementService.GetAllCategoriesForManagement();

            return View(categories);
        }

        [HttpGet]
        public async Task<List<ShopListViewModel>> GetShopLists([FromServices] IShopListService shopListService, int page = 1, int pageSize = 10)
        {
            return await shopListService.GetShopLists(page, pageSize);
        }


        [HttpPost]
        public async Task<bool> UpdateShopListIsCompleteTrue([FromServices] IShopListService shopListService, int orderNumber)
        {
            return await shopListService.UpdateShopListIsCompleteTrue(orderNumber);
        }

        [HttpPost]
        public async Task<bool> CreateOrUpdateProduct(IFormCollection parameters, [FromServices] IManagemenetService managementService)
        {
            return await managementService.CreateOrUpdateProductAsyncManagement(parameters); 
        }

        public async Task<ProductViewModelManagement> GetSingleProductByIdForManagement([FromServices] IManagemenetService managementService, int productId)
        {
            var product = await managementService.GetSingleProductByIdForManagement(productId);

            return product;
        }

        [HttpGet]
        public async Task<List<ProductViewModelManagement>> GetAllProducts([FromServices] IManagemenetService managementService)
        {
            var products = await managementService.GetAllProductsForManagement();

            return products;
        }

        public async Task<CategoryViewModelManagement> GetSingleCategoryByIdForManagement([FromServices] IManagemenetService managementService, int categoryId)
        {
            var product = await managementService.GetSingleCategoryByIdForManagement(categoryId);

            return product;
        }

        [HttpPost]
        public async Task<bool> CreateOrUpdateCategory(IFormCollection parameters, [FromServices] IManagemenetService managementService)
        {
            return await managementService.CreateOrUpdateCategoryAsyncManagement(parameters);
        }

        public async Task<List<CategoryViewModelManagement>> GetAllCategories([FromServices] IManagemenetService managementService)
        {
            var categories = await managementService.GetAllCategoriesForManagement();

            return categories;
        }
    }
}
