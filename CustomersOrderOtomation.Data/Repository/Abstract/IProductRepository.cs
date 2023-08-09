using CustomersOrderOtomation.Data.Models;

namespace CustomersOrderOtomation.Data.Repository.Abstract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        public Task<Product> GetByIdAsync(int id);
        public Task<IEnumerable<Product>> GetAllAsync();

    }
}
