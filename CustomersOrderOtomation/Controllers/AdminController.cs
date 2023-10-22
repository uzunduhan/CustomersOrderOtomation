using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.ShopList;
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
        public List<ShopListViewModel> GetShopLists() 
        {
            return ShopListService.GetAllShopListsWithSignalR();
        }

        public IActionResult AdminProduct()
        {
            return View();
        }
    }
}
