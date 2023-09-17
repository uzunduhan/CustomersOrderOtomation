using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.ViewModel.ShopList;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IShopListService
    {
        Task<ShopListDto> GetSingleShopListByIdAsync(int id);
        Task<List<ShopListViewModel>> GetAllShopLists();
        Task DeleteShopListAsync(int id);
        Task AddShopListAsync(ShopListDto updateResource);
        Task CheckIsCompleteColumnForShopList(ShopList shopList);
        List<ShopListViewModel> GetAllShopListsWithSignalR();
    }
}
