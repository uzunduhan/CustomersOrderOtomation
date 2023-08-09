using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrderOtomation.Data.Repository.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(x => x.Product_Categories).ThenInclude(x => x.Product).Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(x => x.Product_Categories).ThenInclude(x => x.Product).ToListAsync();
        }


        public async Task SafeRemoveAsync(int id)
        {
            var category = await _context.Categories.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (category is null)
                return;

            if (category.IsActive == false)
                return;

            category.IsActive = false;

            _context.Categories.Update(category);

            _context.SaveChanges();



        }
    }
}
