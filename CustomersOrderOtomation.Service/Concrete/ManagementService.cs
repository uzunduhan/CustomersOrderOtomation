using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Http;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class ManagementService : IManagemenetService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IGenericRepository<ProductCategory> productCategoryRepository;
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ManagementService(ICategoryRepository categoryRepository, IProductRepository productRepository, IGenericRepository<ProductCategory> productCategoryRepository,
            IMapper mapper, IProductService productService)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.mapper = mapper;
            this.productService = productService;
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

                float productPrice = parameters.ContainsKey("productPrice") && parameters["productPrice"].Count > 0 &&
                         float.TryParse(parameters["productPrice"][0], out float price)
                         ? price
                         : 0;

                int productCategoryId = Convert.ToInt32(parameters["productCategoryId"][0]);

                var productForExistingControl = await productRepository.GetByIdAsync(productId);

                ProductDto productDto = new ProductDto()
                {
                    Name = productName,
                    Price = productPrice,
                };

                ProductCategory productCategory = new ProductCategory()
                {
                    Product = productForExistingControl.Product_Categories.FirstOrDefault().Product,
                    Category = productForExistingControl.Product_Categories.FirstOrDefault().Category
                };

                productCategory.Category.Id = 1;

                if (productForExistingControl != null)
                {
                    await productService.UpdateProductAsync(productId, productDto);
                    productCategoryRepository.Update(productCategory);
                }
                else
                {
                    await productService.AddProductAsync(productDto);
                    await productCategoryRepository.InsertAsync(productCategory);
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
