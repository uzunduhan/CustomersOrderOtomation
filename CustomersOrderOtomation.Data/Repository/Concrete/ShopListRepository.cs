using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrderOtomation.Data.Repository.Concrete
{
    public class ShopListRepository : GenericRepository<ShopList>, IShopListRepository
    {
        public ShopListRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<ShopList> GetByIdAsyncWithProductsAsync(int id, int userId)
        {
            return await _context.ShopLists.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ShopList>> GetAllWithProductsForAdminAsync()
        {
            return await _context.ShopLists.ToListAsync();
        }
    }
}
