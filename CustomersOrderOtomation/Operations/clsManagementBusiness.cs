using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.ShopList;

namespace CustomersOrderOtomation.Operations
{
    public class clsManagementBusiness
    {
        private readonly IShopListService shopListService;

        public clsManagementBusiness(IShopListService shopListService)
        {
            this.shopListService = shopListService;
        }
        public async Task<bool> UpdateShopListIsCompleteTrue(int orderNumber)
        {
            try
            {
                var shop = await shopListService.GetSingleShopListByIdAsyncPure(orderNumber);

                if (shop != null)
                {
                    shopListService.CheckIsCompleteColumnForShopList(shop);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<ShopListViewModel> GetShopLists(int page, int pageSize)
        {
            var dataList = shopListService.GetAllShopListsWithSignalR().Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dataList;
        }
    }
}
