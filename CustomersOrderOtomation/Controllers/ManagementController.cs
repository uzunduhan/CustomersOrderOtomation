using CustomersOrderOtomation.Service.Abstract;
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
    }
}
