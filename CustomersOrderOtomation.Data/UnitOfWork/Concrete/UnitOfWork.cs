using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Data.Repository.Concrete;
using CustomersOrderOtomation.Data.UnitOfWork.Abstract;

namespace CustomersOrderOtomation.Data.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public bool disposed;



        public IGenericRepository<Product> ProductRepository { get; private set; }
        public IGenericRepository<Category> CategoryRepository { get; private set; }
        public IGenericRepository<ShopList> ShopListRepository { get; private set; }
        public IGenericRepository<ProductCategory> ProductCategoryRepository { get; private set; }
       


        public UnitOfWork(DatabaseContext context)
        {
            _context = context;

            ProductRepository = new GenericRepository<Product>(_context);
            CategoryRepository= new GenericRepository<Category>(_context);
            ShopListRepository= new GenericRepository<ShopList>(_context);
            ProductCategoryRepository= new GenericRepository<ProductCategory>(_context);
          
        }



        public Task CompleteAsync()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // logging                    
                    dbContextTransaction.Rollback();
                }
            }

            return Task.CompletedTask;
        }

        protected virtual void Clean(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}

