using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Category;
using CustomersOrderOtomation.ViewModel.Product;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class ManagementService : IManagemenetService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IGenericRepository<ProductCategory> productCategoryRepository;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IStorageService storageService;

        public ManagementService(ICategoryRepository categoryRepository, IProductRepository productRepository, IGenericRepository<ProductCategory> productCategoryRepository,
            IMapper mapper, IProductService productService, ICategoryService categoryService, IStorageService storageService)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.mapper = mapper;
            this.productService = productService;
            this.categoryService = categoryService;
            this.storageService = storageService;
        }
        public async Task<bool> CreateOrUpdateProductAsyncManagement(IFormCollection parameters)
        {
            try
            {
                var productIdPar = parameters["productId"];
                int productId = 0;

                if (productIdPar.Count > 0)
                {
                    productId = Convert.ToInt32(productIdPar[0]);
                }

                string productName = parameters["productName"][0] ?? "";
                bool productStatus = Convert.ToBoolean(parameters["productStatus"][0]);
                var file = parameters.Files.GetFiles("fileInput").FirstOrDefault();


                double productPrice = parameters.ContainsKey("productPrice") && parameters["productPrice"].Count > 0 &&
                                       double.TryParse(parameters["productPrice"][0], NumberStyles.Any, CultureInfo.CurrentCulture, out double price)
                                       ? price
                                        : 0;


                var productForExistingControl = await productRepository.GetByIdAsync(productId);

                string productImage = await storageService.UploadFileAsync(file);

                ProductDto productDto = new ProductDto()
                {
                    Name = productName,
                    Price = productPrice,
                    ImageUrl = productImage,
                    IsActive = productStatus,
                };


                if (productForExistingControl != null)
                {
                    await productService.UpdateProductAsync(productId, productDto);
                }
                else
                {
                    productDto.CreatedAt = DateTime.Now;    
                    await productService.AddProductAsync(productDto);
                }

            }
            catch (Exception ex)
            {
                return false;
            }



            return true;
        }

        public async Task<List<ProductViewModelManagement>> GetAllProductsForManagement()
        {
            var products = await productRepository.GetAllAsync();

            List<ProductViewModelManagement> vm = mapper.Map<List<ProductViewModelManagement>>(products);

            return vm;
        }

        public async Task<ProductViewModelManagement> GetSingleProductByIdForManagement(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            ProductViewModelManagement vm = mapper.Map<ProductViewModelManagement>(product);

            return vm;
        }

        public async Task<List<CategoryViewModelManagement>> GetAllCategoriesForManagement()
        {
            var categories = await categoryRepository.GetAllAsync();

            List<CategoryViewModelManagement> vm = mapper.Map<List<CategoryViewModelManagement>>(categories);

            return vm;
        }

        public async Task<CategoryViewModelManagement> GetSingleCategoryByIdForManagement(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            CategoryViewModelManagement vm = mapper.Map<CategoryViewModelManagement>(category);

            return vm;
        }

        public async Task<bool> CreateOrUpdateCategoryAsyncManagement(IFormCollection parameters)
        {
            try
            {
                var categoryIdPar = parameters["categoryId"];
                int categoryId = 0;

                if (categoryIdPar.Count > 0)
                {
                    categoryId = Convert.ToInt32(categoryIdPar[0]);
                }

                string categoryName = parameters["categoryName"][0] ?? "";
                bool categoryStatus = Convert.ToBoolean(parameters["categoryStatus"][0]);  


                var categoryForExistingControl = await categoryRepository.GetByIdAsync(categoryId);

                CategoryDto categoryDto = new CategoryDto()
                {
                    Name = categoryName,
                    IsActive = categoryStatus,
                };


                if (categoryForExistingControl != null)
                {
                    await categoryService.UpdateCategoryAsync(categoryId, categoryDto);
                }
                else
                {
                    categoryDto.CreatedAt = DateTime.Now;   
                    await categoryService.AddCategoryAsync(categoryDto);
                }


            }
            catch (Exception ex)
            {
                return false;
            }



            return true;
        }
    }
}
