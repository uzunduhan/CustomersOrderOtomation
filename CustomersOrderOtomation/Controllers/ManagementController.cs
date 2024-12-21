using CustomersOrderOtomation.Service.Abstract;
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

        public async Task<IActionResult> ManageProduct([FromServices] IProductService productService)
        {
            var products = await productService.GetAllProductsForManagement();

            return View(products);
        }

        public async Task<IActionResult> ManageCategory([FromServices] ICategoryService categoryService)
        {
            var categories = await categoryService.GetAllCategoriesForManagement();

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
        public async Task<bool> CreateOrUpdateProduct(IFormCollection parameters, [FromServices] IProductService productService)
        {
            return await productService.CreateOrUpdateProductAsync(parameters); 
        }

        public async Task<ProductViewModelManagement> GetSingleProductByIdForManagement([FromServices] IProductService productService, int productId)
        {
            var product = await productService.GetSingleProductByIdForManagement(productId);

            return product;
        }

        [HttpGet]
        public async Task<List<ProductViewModelManagement>> GetAllProducts([FromServices] IProductService productService)
        {
            var products = await productService.GetAllProductsForManagement();

            return products;
        }
    }
}
