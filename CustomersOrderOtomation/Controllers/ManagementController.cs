using AutoMapper;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.ShopList;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IMapper mapper;

        public IShopListService ShopListService { get; }

        public ManagementController(IShopListService shopListService, IMapper mapper)
        {
            ShopListService = shopListService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<ShopListViewModel> GetShopLists(int page = 1, int pageSize = 10)
        {
            var dataList = ShopListService.GetAllShopListsWithSignalR().Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dataList;
        }

        public IActionResult ManageProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> UpdateShopListIsCompleteTrue(int orderNumber)
        {
            try
            {
                var shop = await ShopListService.GetSingleShopListByIdAsyncPure(orderNumber);

                if (shop != null)
                {
                    ShopListService.CheckIsCompleteColumnForShopList(shop);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }




        }
    }
}
