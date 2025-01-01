using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Data.Repository.Concrete;
using CustomersOrderOtomation.Data.UnitOfWork.Abstract;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Category;
using CustomersOrderOtomation.ViewModel.Product;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> genericRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IGenericRepository<Category> genericRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddCategoryAsync(CategoryDto updateResource)
        {
            var category = mapper.Map<CategoryDto, Category>(updateResource);

            await genericRepository.InsertAsync(category);
            await unitOfWork.CompleteAsync();
        }

        public async Task SafeDeleteCategorytAsync(int id)
        {
            var category = await genericRepository.GetByIdAsync(id);

            if (category is null)
                throw new InvalidOperationException("category not found");

            await categoryRepository.SafeRemoveAsync(id);
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            List<CategoryViewModel> vm = mapper.Map<List<CategoryViewModel>>(categories);

            return vm;
        }

        public async Task<CategoryDetailViewModel> GetCategoryById(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            CategoryDetailViewModel vm = mapper.Map<CategoryDetailViewModel>(category);

            return vm;


        }

        public async Task UpdateCategoryAsync(int id, CategoryDto updateResource)
        {
            var category = await genericRepository.GetByIdAsync(id);

            if (category is null)
                throw new InvalidOperationException("category not found");

            var mapped = mapper.Map(updateResource, category);

            genericRepository.Update(mapped);
            await unitOfWork.CompleteAsync();
        }

    }
}
