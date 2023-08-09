using CustomersOrderOtomation.Data.Models;

namespace CustomersOrderOtomation.Data.Repository.Abstract
{
    public interface IShopListRepository : IGenericRepository<ShopList>
    {
        Task<ShopList> GetByIdAsyncWithProductsAsync(int id, int userId);
        Task<IEnumerable<ShopList>> GetAllWithProductsForAdminAsync();

    }
}
