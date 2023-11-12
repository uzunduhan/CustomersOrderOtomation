using CustomersOrderOtomation.Data.Models;

namespace CustomersOrderOtomation.Data.Repository.Abstract
{
    public interface IShopListRepository : IGenericRepository<ShopList>
    {
        Task<ShopList> GetByIdAsyncWithProductsAsync(int id);
        Task<IEnumerable<ShopList>> GetAllWithProductsForAdminAsync();
        List<ShopList> GetAllShopListWithSıgnalR();

    }
}
