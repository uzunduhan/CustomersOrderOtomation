using CustomersOrderOtomation.Operations;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.ShopList;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class ManagementController : Controller
    {

        public IShopListService ShopListService { get; }

        public ManagementController(IShopListService shopListService)
        {
            ShopListService = shopListService;
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
        public List<ShopListViewModel> GetShopLists(int page = 1, int pageSize = 10)
        {
            clsManagementBusiness managementBusiness = new clsManagementBusiness(ShopListService);
            return managementBusiness.GetShopLists(page, pageSize);
        }


        [HttpPost]
        public async Task<bool> UpdateShopListIsCompleteTrue(int orderNumber)
        {
            clsManagementBusiness managementBusiness = new clsManagementBusiness(ShopListService);
            return await managementBusiness.UpdateShopListIsCompleteTrue(orderNumber);
        }
    }
}
