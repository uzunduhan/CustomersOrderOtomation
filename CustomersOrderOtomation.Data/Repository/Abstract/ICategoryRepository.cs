using CustomersOrderOtomation.Data.Models;

namespace CustomersOrderOtomation.Data.Repository.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task SafeRemoveAsync(int id);
    }
}
