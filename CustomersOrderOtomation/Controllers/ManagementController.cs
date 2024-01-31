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

        public IActionResult ManageProduct()
        {
            return View();
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
    }
}
