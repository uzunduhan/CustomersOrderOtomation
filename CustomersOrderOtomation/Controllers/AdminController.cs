using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class AdminController : Controller
    {
        public IShopListService ShopListService { get; }

        public AdminController(IShopListService shopListService) 
        {
            ShopListService = shopListService;
        }
        public async Task<IActionResult> Index()
        {
            var shopLists = await ShopListService.GetAllShopLists();
            shopLists = shopLists.OrderByDescending(x => x.Id).ToList();
            return View(shopLists);
        }

        public IActionResult AdminProduct()
        {
            return View();
        }
    }
}
