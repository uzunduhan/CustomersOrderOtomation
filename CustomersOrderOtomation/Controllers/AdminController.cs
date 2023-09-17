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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetShopLists() 
        {
            return Ok(ShopListService.GetAllShopListsWithSignalR());
        }

        public IActionResult AdminProduct()
        {
            return View();
        }
    }
}
