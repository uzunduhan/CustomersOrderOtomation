using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;

namespace CustomersOrderOtomation.Data.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<ShopList> ShopListRepository { get; }
        IGenericRepository<ProductCategory> ProductCategoryRepository { get; }

        Task CompleteAsync();
    }
}
